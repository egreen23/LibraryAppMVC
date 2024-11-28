using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace LibraryAppMVC.Utility
{
    public class CheckMigrations
    {
        private readonly DbContext _dbContext;

        public CheckMigrations(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void CheckAndWriteToFile(string migrations_directoryPath, string output_filepath)
        {
            var appliedMigrations = _dbContext.Database.GetAppliedMigrations().ToList();

            // Get all migration filenames from the specified folder
            var migrationFiles = Directory.GetFiles(migrations_directoryPath, "*.sql");

            var migrationResults = new List<string>();

            foreach (var file in migrationFiles)
            {
                // Extract the migration name from the filename (assuming filename format is "<timestamp>_<MigrationName>.sql")
                var migrationName = Path.GetFileNameWithoutExtension(file);

                bool isApplied = appliedMigrations.Contains(migrationName);

                // Write the result in the desired format
                migrationResults.Add($"{migrationName} {isApplied}");
                
            }

            // Write all results to the output file
            File.WriteAllLines(output_filepath, migrationResults);
        }
    }
}
