//class from: http://www.codeproject.com/Articles/11130/String-Enumerations-in-C
//license: http://www.codeproject.com/info/cpol10.aspx

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CTCT.Util
{
    /// <summary>
    /// Simple attribute class for storing String Values
    /// </summary>
    public class StringValueAttribute : Attribute
    {
        private string _value;

        /// <summary>
        /// Creates a new <see cref="StringValueAttribute"/> instance.
        /// </summary>
        /// <param name="value">Value.</param>
        public StringValueAttribute(string value)
        {
            _value = value;
        }

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <value></value>
        public string Value
        {
            get { return _value; }
        }
    }
}
