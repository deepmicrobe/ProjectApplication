using Microsoft.Extensions.Options;
using ProjectApplication.Helpers;
using ProjectApplication.Models;
using System.Data.SqlClient;
using System.Text;

namespace ProjectApplication.Services
{
    public interface INotesService
    {
        void CreateNote(CreateNoteRequest createNoteRequest);
        List<GetNotesResponse> GetNotes(int? projectId, List<int>? attributeIds);
    }

    public class NotesService : INotesService
    {
        private readonly AppSettings _appSettings;
        private string _connectionString;

        public NotesService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
            _connectionString = ConnectionHelper.GetConnectionString(_appSettings);
        }

        public void CreateNote(CreateNoteRequest createNoteRequest)
        {
            if (createNoteRequest == null)
            {
                return;
            }

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var id = InsertNote(connection, createNoteRequest);

                InsertNoteAttributes(connection, id, createNoteRequest.Attributes);
            }
        }

        public List<GetNotesResponse> GetNotes(int? projectId, List<int>? attributeIds)
        {
            var notes = new List<GetNotesResponse>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                var path = Path.Combine(Environment.CurrentDirectory, @"Data\GetNotes.sql");

                var getNotesQuery = File.ReadAllText(path);

                var sb = new StringBuilder();
                sb.Append(getNotesQuery);

                var filterByProject = projectId.GetValueOrDefault() > 0;
                var filterByAttributes = attributeIds != null && attributeIds.Any();
                var needAndOperator = filterByProject && filterByAttributes;

                if (filterByAttributes)
                {
                    sb.AppendLine("INNER JOIN dbo.NoteAttributes dna ON dna.NoteId = dn.NoteId");
                }

                if (filterByProject || filterByAttributes)
                {
                    sb.AppendLine("WHERE");

                    if (needAndOperator)
                    {
                        sb.AppendLine($"dn.ProjectId = {projectId} AND dna.NoteId IN (");
                    }
                    else if(filterByProject)
                    {
                        sb.AppendLine($"dn.ProjectId = {projectId}");
                    }
                    else if(filterByAttributes)
                    {
                        sb.AppendLine($"dna.NoteId IN (");

                        var lastIndex = attributeIds.Count - 1;

                        for (var i = 0; i < attributeIds.Count; i++)
                        {
                            sb.Append(attributeIds[i]);

                            if (i < lastIndex)
                            {
                                sb.Append(", ");
                            }
                            else
                            {
                                sb.Append(")");
                            }
                        }
                    }
                }

                using (var command = new SqlCommand(sb.ToString(), connection))
                {
                    connection.Open();

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            notes.Add(new GetNotesResponse { NoteId = reader.GetInt32(0), Text = reader.GetString(1) });
                        }
                    }
                }
            }

            return notes;
        }

        private int InsertNote(SqlConnection connection, CreateNoteRequest createNoteRequest)
        {
            var path = Path.Combine(Environment.CurrentDirectory, @"Data\CreateNote.sql");

            var createNoteQuery = File.ReadAllText(path);

            var id = -1;

            using (var command = new SqlCommand(createNoteQuery, connection))
            {
                command.Parameters.Add("@text", System.Data.SqlDbType.VarChar);
                command.Parameters["@text"].Value = createNoteRequest.Text;

                command.Parameters.Add("@creation", System.Data.SqlDbType.DateTime);
                command.Parameters["@creation"].Value = DateTime.UtcNow;

                command.Parameters.Add("@projectId", System.Data.SqlDbType.Int);
                command.Parameters["@projectId"].Value = createNoteRequest.ProjectId;

                var val = command.ExecuteScalar();

                if (val != null)
                {
                    int.TryParse(val.ToString(), out id);
                }
            }

            return id;
        }

        private void InsertNoteAttributes(SqlConnection connection, int id, List<int> attributes)
        {
            if (attributes != null && attributes.Any())
            {
                var sb = new StringBuilder();
                sb.Append("INSERT INTO dbo.NoteAttributes (NoteId, AttributeId) VALUES ");

                // TODO: batch add attributes
                var lastIndex = attributes.Count - 1;

                for (var i = 0; i < attributes.Count; i++)
                {
                    if (i < lastIndex)
                    {
                        sb.AppendLine($"({id}, {attributes[i]}),");
                    }
                    else
                    {
                        sb.AppendLine($"({id}, {attributes[i]})");
                    }
                }

                using (var command = new SqlCommand(sb.ToString(), connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
