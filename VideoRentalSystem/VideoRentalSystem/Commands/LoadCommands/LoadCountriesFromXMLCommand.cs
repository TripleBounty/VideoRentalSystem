﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using VideoRentalSystem.Commands.Contracts;
using VideoRentalSystem.Common.Contracts;
using VideoRentalSystem.Data.Contracts;
using VideoRentalSystem.Models;
using VideoRentalSystem.Models.Factories;

namespace VideoRentalSystem.Commands.CreateCommands
{
    public class LoadCountriesFromXMLCommand : ICommand
    {
        private readonly IDatabase db;
        private readonly IModelsFactory factory;
        private readonly IWriter writer;

        public LoadCountriesFromXMLCommand(IDatabase db, IModelsFactory factory, IWriter writer)
        {
            this.db = db;
            this.factory = factory;
            this.writer = writer;
        }

        public string Execute(IList<string> parameters)
        {
            if (parameters.Count != 1)
            {
                return "Not valid number of parameters";
            }

            string path = "../../Loaders/xml/";
            string fileName = parameters[0];

            string fileLoc = $"{path}{fileName}.xml";

            if (!File.Exists(fileLoc))
            {
                return $"File {fileLoc} does not exist";
            }

            string code;
            string countryName;

            var countryList = new List<Country>();
            Country country;

            XmlReaderSettings xmlSettings = new XmlReaderSettings();
            xmlSettings.Schemas = new System.Xml.Schema.XmlSchemaSet();
            xmlSettings.Schemas.Add("", "../../Loaders/xml/countries.xsd");
            xmlSettings.ValidationType = ValidationType.Schema;

            using (XmlReader reader = XmlReader.Create(fileLoc, xmlSettings))
            {
                try
                {
                    while (reader.Read())
                    {
                        if (reader.NodeType == XmlNodeType.Element && (reader.Name == "country"))
                        {
                            code = reader.GetAttribute("code");
                            if (code != null)
                            {
                                countryName = reader.ReadElementString();
                                try
                                {
                                    country = this.factory.CreateCountry(countryName, code);
                                    countryList.Add(country);
                                }
                                catch (Exception)
                                {
                                    this.writer.WriteLine($"Country with code: {code} cannot be created");
                                }
                            }
                        }
                    }
                }
                catch (Exception)
                {
                    return "XML is not in correct format";
                }
            }

            this.db.Countries.AddRange(countryList);
            this.db.Complete();

            return "File treated";
        }
    }
}