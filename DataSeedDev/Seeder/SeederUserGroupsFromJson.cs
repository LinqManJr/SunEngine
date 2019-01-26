﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using LinqToDB;
using Newtonsoft.Json.Linq;
using SunEngine.Commons.DataBase;
using SunEngine.Commons.Models;
using SunEngine.Commons.Models.UserGroups;
using SunEngine.Commons.Services;

namespace SunEngine.Seeder
{
    public class SeederUserGroupsFromJson
    {
        private readonly DataContainer dataContainer;

        public SeederUserGroupsFromJson(DataContainer dataContainer)
        {
            this.dataContainer = dataContainer;
        }

        public void Seed(string fileName)
        {
            IList<string> allSuperKeys = OperationKeysContainer.GetAllSuperKeys();
            
            string jsonText = File.ReadAllText(fileName);
            JArray groupsJson = JArray.Parse(jsonText);
            foreach (JObject userGroupJson in groupsJson)
            {
                int id = dataContainer.NextUserGroupId();
                UserGroupDB userGroupDb = new UserGroupDB
                {
                    Id = id,
                    Name = (string) userGroupJson["Name"],
                    Title = (string) userGroupJson["Title"],
                    IsSuper = userGroupJson.ContainsKey("IsSuper") && (bool) userGroupJson["IsSuper"],
                    SortNumber = id
                };
                userGroupDb.NormalizedName = userGroupDb.Name.ToUpper();

                dataContainer.UserGroups.Add(userGroupDb);

                var categoriesAccessJsonList = (JArray) userGroupJson["Categories"];
                if (categoriesAccessJsonList != null)
                {
                    foreach (var categoriesAccessJson in categoriesAccessJsonList)
                    {
                        string name = (string) categoriesAccessJson["Category"];
                        Category category = dataContainer.Categories.FirstOrDefault(x => x.Name == name);
                        if (category == null)
                        {
                            throw new Exception("No such category: " + name);
                        }

                        CategoryAccessDB categoryAccessDb = new CategoryAccessDB
                        {
                            Id = dataContainer.NextCategoryAccessId(),
                            CategoryId = category.Id,
                            UserGroupId = userGroupDb.Id
                        };

                        dataContainer.CategoryAccesses.Add(categoryAccessDb);

                        var operationKeysJsonObject = (JObject) categoriesAccessJson["OperationKeys"];

                        foreach (var operationKeyJson in operationKeysJsonObject.Properties())
                        {                            
                            string keyName = operationKeyJson.Name;

                            if (!userGroupDb.IsSuper && allSuperKeys.Contains(keyName))
                            {
                                throw new Exception($"Ordinary UserGroup '{userGroupDb.Name}' can not contain IsSuper key '{keyName}'");
                            }
                            
                            var operationKey = dataContainer.OperationKeys.FirstOrDefault(x => x.Name == keyName);
                            if (operationKey == null)
                            {
                                throw new Exception("No such key in registered keys: " + keyName);
                            }
                            
                            CategoryOperationAccessDB categoryOperationAccessDb = new CategoryOperationAccessDB
                            {
                                CategoryAccessId = categoryAccessDb.Id,
                                OperationKeyId = operationKey.OperationKeyId,
                                Access = (bool) operationKeyJson.Value
                            };

                            dataContainer.CategoryOperationAccesses.Add(categoryOperationAccessDb);
                        }
                    }
                }
            }
        }
    }
}