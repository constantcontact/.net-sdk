using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using CTCT.Util;
using CTCT.Components.Contacts;

namespace CTCT.Components.Activities
{
    /// <summary>
    /// Represents an AddContact activity class.
    /// </summary>
    [DataContract]
    [Serializable]
    public class AddContacts : Component
    {
        [DataMember(Name = "import_data", EmitDefaultValue = false)]
        private List<AddContactsImportData> _ImportData = new List<AddContactsImportData>();

        [DataMember(Name = "lists", EmitDefaultValue = false)]
        private List<string> _Lists = new List<string>();

        [DataMember(Name = "column_names")]
        private List<string> _ColumnNames = new List<string>();

        /// <summary>
        /// Activity id.
        /// </summary>
        [DataMember(Name = "id", EmitDefaultValue = false)]
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the list of imported data.
        /// </summary>
        public IList<AddContactsImportData> ImportData
        {
            get { return _ImportData; }
            set { _ImportData = value == null ? null : value.ToList(); }
        }

        /// <summary>
        /// Gets or sets the list of id's to add.
        /// </summary>
        public IList<string> Lists
        {
            get { return _Lists; }
            set { _Lists = value == null ? null : value.ToList(); }
        }

        /// <summary>
        /// Gets or sets the list of column names.
        /// </summary>
        public IList<string> ColumnNames
        {
            get { return _ColumnNames; }
            set { _ColumnNames = value == null ? null : value.ToList(); }
        }

        /// <summary>
        /// Gets or sets the contact count that were processed by this activity.
        /// </summary>
        [DataMember(Name = "contact_count", EmitDefaultValue = false)]
        public string ContactCount { get; set; }

        /// <summary>
        /// Gets or sets the activity process error count.
        /// </summary>
        [DataMember(Name = "error_count", EmitDefaultValue = false)]
        public string ErrorCount { get; set; }

        /// <summary>
        /// Activity type.
        /// </summary>
        [DataMember(Name = "type", EmitDefaultValue = false)]
        public string Type { get; private set; }

        /// <summary>
        /// Class constructor.
        /// </summary>
        /// <param name="contacts">List of contacts.</param>
        /// <param name="lists">List of id's.</param>
        /// <param name="columnNames">Column names to import.</param>
        public AddContacts(IList<AddContactsImportData> contacts, IList<string> lists, IList<string> columnNames)
        {
            if (contacts != null && contacts.Count > 0)
            {
                this.ImportData = contacts;
                this.Lists = lists;
                this.ColumnNames = columnNames;
                if (columnNames == null || columnNames.Count == 0)
                {
                    this.ColumnNames = new List<string>();
                    // Attempt to determine the column names being used if they are not provided
                    this.ColumnNames.Add(Settings.ActivitiesColumns.Default.Email);
                    if (!String.IsNullOrEmpty(contacts[0].FirstName))
                    {
                        this.ColumnNames.Add(Settings.ActivitiesColumns.Default.FirstName);
                    }
                    if (!String.IsNullOrEmpty(contacts[0].MiddleName))
                    {
                        this.ColumnNames.Add(Settings.ActivitiesColumns.Default.MiddleName);
                    }
                    if (!String.IsNullOrEmpty(contacts[0].LastName))
                    {
                        this.ColumnNames.Add(Settings.ActivitiesColumns.Default.LastName);
                    }
                    if (!String.IsNullOrEmpty(contacts[0].JobTitle))
                    {
                        this.ColumnNames.Add(Settings.ActivitiesColumns.Default.JobTitle);
                    }
                    if (!String.IsNullOrEmpty(contacts[0].CompanyName))
                    {
                        this.ColumnNames.Add(Settings.ActivitiesColumns.Default.CompanyName);
                    }
                    if (!String.IsNullOrEmpty(contacts[0].WorkPhone))
                    {
                        this.ColumnNames.Add(Settings.ActivitiesColumns.Default.WorkPhone);
                    }
                    if (!String.IsNullOrEmpty(contacts[0].HomePhone))
                    {
                        this.ColumnNames.Add(Settings.ActivitiesColumns.Default.HomePhone);
                    }

                    // Addresses
                    Address addr = (from c in contacts where c.Addresses.Count > 0 select c.Addresses[0]).FirstOrDefault();
                    if (addr != null)
                    {
                        if (!String.IsNullOrEmpty(addr.Line1))
                        {
                            this.ColumnNames.Add(Settings.ActivitiesColumns.Default.Address1);
                        }
                        if (!String.IsNullOrEmpty(addr.Line2))
                        {
                            this.ColumnNames.Add(Settings.ActivitiesColumns.Default.Address2);
                        }
                        if (!String.IsNullOrEmpty(addr.Line3))
                        {
                            this.ColumnNames.Add(Settings.ActivitiesColumns.Default.Address3);
                        }
                        if (!String.IsNullOrEmpty(addr.City))
                        {
                            this.ColumnNames.Add(Settings.ActivitiesColumns.Default.City);
                        }
                        if (!String.IsNullOrEmpty(addr.StateCode))
                        {
                            this.ColumnNames.Add(Settings.ActivitiesColumns.Default.State);
                        }
                        if (!String.IsNullOrEmpty(addr.CountryCode))
                        {
                            this.ColumnNames.Add(Settings.ActivitiesColumns.Default.Country);
                        }
                        if (!String.IsNullOrEmpty(addr.PostalCode))
                        {
                            this.ColumnNames.Add(Settings.ActivitiesColumns.Default.PostalCode);
                        }
                        if (!String.IsNullOrEmpty(addr.SubPostalCode))
                        {
                            this.ColumnNames.Add(Settings.ActivitiesColumns.Default.SubPostalCode);
                        }
                    }

                    // Custom fields
                    if (contacts[0].CustomFields != null)
                    {
                        foreach (CustomField field in contacts[0].CustomFields)
                        {
                            this.ColumnNames.Add(field.Name.ToUpper());
                        }
                    }
                }
            }
        }
    }
}
