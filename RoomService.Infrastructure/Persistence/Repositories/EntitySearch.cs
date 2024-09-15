using Microsoft.EntityFrameworkCore;
using System.Text;

namespace RoomService.Infrastructure.Persistence.Repositories
{
    public class EntitySearchHelper<T>
    where T : class
    {
        private readonly DbContext _context;

        public EntitySearchHelper(DbContext context)
        {
            _context = context;
        }

        public IQueryable<T> SearchEntity(string keyword)
        {
            var entityType = _context.Model.FindEntityType(typeof(T));
            var existingColumns = entityType.GetProperties().Select(p => p.GetColumnName()).ToList();
            var columnToPropertyMap = entityType.GetProperties().ToDictionary(p => p.GetColumnName(), p => p.Name);

            var stringColumns = existingColumns.Where(col =>
            {
                if (columnToPropertyMap.TryGetValue(col, out var propertyName))
                {
                    var property = entityType.FindProperty(propertyName);
                    return property != null && property.ClrType == typeof(string);
                }
                else
                {
                    return false;
                }
            });

            var searchTermLower = keyword.ToLower();
            var queryBuilder = new StringBuilder();
            queryBuilder.Append($"SELECT * FROM {entityType.GetTableName()} WHERE ");

            foreach (var column in stringColumns)
            {
                queryBuilder.Append($"LOWER({column}) COLLATE \"C\" ILIKE '%' || @searchKeyword || '%' OR ");
            }
            queryBuilder.Remove(queryBuilder.Length - 4, 4);

            return _context.Set<T>().FromSqlRaw(queryBuilder.ToString(), new NpgsqlParameter("@searchKeyword", searchTermLower));
        }
    }
}
