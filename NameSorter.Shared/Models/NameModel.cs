using System.Collections.Generic;
using System.Linq;

namespace NameSorter.Shared.Models
{
    public class NameModel
    {
        public NameModel()
        {
            // Ensure the list has a minimum of two elements
            // to facilitate with sorting of name lists
            GivenNames = new List<string> { "", "" };
        }

        #region Fields

        public string FirstName { get; set; }

        public string LastName { get; set; }

        private List<string> _givenNames;
        public List<string> GivenNames
        {
            get => _givenNames;
            
            set
            {
                if (value == null || !value.Any())
                    return;

                _givenNames = value;

                // Add a second empty string
                // to facilitate with sorting of names
                if (_givenNames.Count < 2)
                    _givenNames.Add("");
            }
        }

        #endregion

        public bool Validate()
        {
            // a name model must have
            // a last name and at least 1 given name
            if (string.IsNullOrEmpty(FirstName) ||
                string.IsNullOrEmpty(LastName))
                return false;


            // a name model may have
            // up to 2 additional given names
            if (GivenNames != null &&
                GivenNames.Count != 2)
                return false;

            return true;
        }

        public override string ToString()
        {
            var givenNamesStr = string.Empty;
            foreach (var givenName in GivenNames)
            {
                if (!string.IsNullOrEmpty(givenName))
                    givenNamesStr += givenName + " ";
            }

            return $"{FirstName} {givenNamesStr}{LastName}";
        }
    }
}
