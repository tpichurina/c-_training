﻿using Newtonsoft.Json;
using NUnit.Framework;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using System;

namespace webAddressbookTests
{
    [TestFixture]
    public class GroupCreationTests : GroupTestbase
    {
        public static IEnumerable<GroupData> RandomGroupDataProvider()
        {
            List<GroupData> groups = new List<GroupData>();
            for (int i = 0; i < 5; i++)
            {
                groups.Add(new GroupData(GenerateRandomString(30))
                {
                    Header = GenerateRandomString(100),
                    Footer = GenerateRandomString(100)

                });
            }
            return groups;
        }

        public static IEnumerable<GroupData> GroupDataFromCsvFile()
        {
            List<GroupData> groups = new List<GroupData>();
            string path = TestContext.CurrentContext.TestDirectory;
            string[] lines = File.ReadAllLines(path + "\\groups.csv");
            foreach (string l in lines)
            {
                string[] parts = l.Split(',');
                groups.Add(new GroupData(parts[0])
                {
                    Header = parts[1],
                    Footer = parts[2]
                });
            }
            return groups;
        }

        public static IEnumerable<GroupData> GroupDataFromXmlFile()
        {
            string path = TestContext.CurrentContext.TestDirectory;
            return (List<GroupData>)
                new XmlSerializer(typeof(List<GroupData>))
                   .Deserialize(new StreamReader(path + "\\groups.xml"));
        }

        public static IEnumerable<GroupData> GroupDataFromJsonFile()
        {
            string path = TestContext.CurrentContext.TestDirectory;
            return JsonConvert.DeserializeObject<List<GroupData>>(
                File.ReadAllText(path + "\\groups.json"));
        }

        [Test, TestCaseSource("GroupDataFromXmlFile")]
        public void GroupCreationTest(GroupData group)
        {
            List<GroupData> oldGroups = GroupData.GetAll();

            app.Groups.Create(group);

            List<GroupData> newGroups = GroupData.GetAll();
            oldGroups.Add(group);
            Assert.AreEqual(oldGroups, newGroups);
        }


        [Test]
        public void TestDBConnectivity()
        {
            foreach (ContactData contact in ContactData.GetAll())
            {
                System.Console.Out.WriteLine(contact.Deprecated);
            }
        }
    }
}
