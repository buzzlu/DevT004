﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata;
using Npgsql;
using Npgsql.NameTranslation;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;

namespace WebMVC
{
   public class ConvertToSnakeCase
    {
        private static readonly Regex _keysRegex = new Regex("^(PK|FK|IX)_", RegexOptions.Compiled);

        public static void FixSnakeCaseNames(ModelBuilder modelBuilder)
        {
            var mapper = new NpgsqlSnakeCaseNameTranslator();
            foreach (var table in modelBuilder.Model.GetEntityTypes())
            {
                ConvertToSnake(mapper, table);
                foreach (var property in table.GetProperties())
                {
                    ConvertToSnake(mapper, property);
                }

                foreach (var primaryKey in table.GetKeys())
                {
                    ConvertToSnake(mapper, primaryKey);
                }

                foreach (var foreignKey in table.GetForeignKeys())
                {
                    ConvertToSnake(mapper, foreignKey);
                }

                foreach (var indexKey in table.GetIndexes())
                {
                    ConvertToSnake(mapper, indexKey);
                }
            }
        }

        private static void ConvertToSnake(INpgsqlNameTranslator mapper, object entity)
        {
            switch (entity)
            {
                case IMutableEntityType table:
                    var relationalTable = table.Relational();
                    relationalTable.TableName = ConvertGeneralToSnake(mapper, relationalTable.TableName);
                    if (relationalTable.TableName.StartsWith("asp_net_"))
                    {
                        relationalTable.TableName = relationalTable.TableName.Replace("asp_net_", string.Empty);
                        relationalTable.Schema = "identity";
                    }

                    break;
                case IMutableProperty property:
                    property.Relational().ColumnName = ConvertGeneralToSnake(mapper, property.Relational().ColumnName);
                    break;
                case IMutableKey primaryKey:
                    primaryKey.Relational().Name = ConvertKeyToSnake(mapper, primaryKey.Relational().Name);
                    break;
                case IMutableForeignKey foreignKey:
                    foreignKey.Relational().Name = ConvertKeyToSnake(mapper, foreignKey.Relational().Name);
                    break;
                case IMutableIndex indexKey:
                    indexKey.Relational().Name = ConvertKeyToSnake(mapper, indexKey.Relational().Name);
                    break;
                default:
                    throw new NotImplementedException("Unexpected type was provided to snake case converter");
            }
        }

        private static string ConvertKeyToSnake(INpgsqlNameTranslator mapper, string keyName) =>
            ConvertGeneralToSnake(mapper, _keysRegex.Replace(keyName, match => match.Value.ToLower()));

        private static string ConvertGeneralToSnake(INpgsqlNameTranslator mapper, string entityName) =>
            mapper.TranslateMemberName(ModifyNameBeforeConvertion(mapper, entityName));

        protected static string ModifyNameBeforeConvertion(INpgsqlNameTranslator mapper, string entityName) => entityName;

    }
}
