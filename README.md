# sqlserver2postgre
C# Console application for data migration from SQLServer to PostgreSQL (+Postgis)

Very simple tool to migrate data. (Tool written on the fly, at the moment it satisfies my needs)
Supported types:
- INT
- GEOMETRY

Configuration required in App.config:
- WORK_DIR: directory where schema files will be read (json files)
- CONNECTION PARAMETERS OF PG & MSSQL DATABASES

Schema files are a simple json where the content describes a column mapping (Source, Destination). 
Here an example (already included in solution migration_config_sample.json):
```json
{
  "sourceTable": {
    "tableName": "SpatialTable",
    "columns": [
      "id",
      "GeomCol1"
    ],
    "recordLimit": 0
  },
  "destinationTable": {
    "truncateBeforeInsert": false,    
    "tableName": "geo_line",
    "columns": [
      "id_geo_line",
      "geom" // https://postgis.net/docs/UpdateGeometrySRID.html
    ],
    "geometrySRID": 32632 // srid of geometry fields
  }  
}
```
The mapping works by position so, in this example "id" is mapped in "id_geo_line" and "GeomCol1" is mapped in "geom"
