using System;
using System.ComponentModel;
using System.Globalization;
using System.Linq;

namespace SwedishNationalId
{
    /// <summary>
    /// 
    /// </summary>
    public enum NationalIdTypes
    {
        SSN = 1, // social security number
        CIN = 2, // corporate identity number
    }



    /// <summary>
    /// Represents Swedish Personal Identification Number and Organization Number.
    /// Handles the following formats: YYMMDD-CCCC YYMMDDCCCC, YYYYMMDD-CCCC and YYYYMMDDCCCC    /// 
    /// </summary>
    public class NationalId
    {
        internal string _id;
        internal NationalIdTypes? _type;
        
        internal NationalId()
        {
            _id = string.Empty;
            _type = null;
        }

        public NationalId(string nationalId)
        {
            if (nationalId == null)
            {
                throw new ArgumentNullException(nameof(nationalId));
            }
                        
            var nid = NationalId.Parse(nationalId);
            this._id = nid._id;
            this._type = nid._type;
        }

        public static NationalId Parse(string input)
        {
            bool isOk = TryParse(input, out NationalId nationalId);

            if (isOk)
                return nationalId;
            else
            {
                throw new FormatException("Wrong format of NationalId");
            }
        }

        public static bool TryParse(string input, out NationalId result)
        {
            result = new NationalId();

            string copy = input.Trim();
            var length = copy.Length;
            if (length < 10 || length > 13)
                return false;

            var firstHyphenPosition = copy.IndexOf('-');
            var lastHyphenPosition = copy.LastIndexOf('-');

            // only one hyphen allowed
            if (firstHyphenPosition != lastHyphenPosition)
                return false;

            //check if hyphen is in the correct position
            if (lastHyphenPosition != -1 && (lastHyphenPosition + 1) != length - 4)
                return false;

            copy = copy.Replace("-", "");
            length = copy.Length;
            if (length != 10 && length != 12)
                return false;

            foreach (char symb in copy)
                if (!char.IsDigit(symb))
                    return false;                        

            int s = 0;
            if (length == 12)
            {                
                s = int.Parse(copy[4].ToString());
                if (s > 1)
                    return false;
            }
            else
                s = int.Parse(copy[2].ToString());

            bool isSsn = false;
            if (s <= 1)
                isSsn = true;

            // make sure SSN has 12 chars
            if (isSsn && length == 10)
            {
                int year = int.Parse(copy.Substring(0, 2));
                int currentYear = int.Parse(DateTime.Today.ToString("yy"));
                
                if (year < currentYear)                
                    copy = "20" + copy;                
                else
                    copy = "19" + copy;
            }            

            result._id = copy;
            result._type = isSsn ? NationalIdTypes.SSN : NationalIdTypes.CIN;

            return true;
        }
        
        
        public virtual bool IsValid()
        {
            return !string.IsNullOrEmpty(_id);
        }

        public bool IsSSN { get => this._type == NationalIdTypes.SSN; }

        public bool IsCIN { get => this._type == NationalIdTypes.CIN; }

        public override string ToString()
        {
            return _id;
        }
        
    }

    /// <summary>
    /// Immutable class that represents swedish personal identificatio number.
    /// Understands formats YYMMDD-CCCC, YYMMDDCCCC, YYYYMMDD-CCCC and YYYYMMDDCCCC.
    /// Uses format YYYYMMDDCCCC when converting to string.
    /// </summary>
    public class Ssn : NationalId
    {        
        public Ssn(string ssn)
        {
            if (ssn == null)
            {
                throw new ArgumentNullException(nameof(ssn));
            }

            var nid = NationalId.Parse(ssn);

            if (!nid.IsSSN)
                throw new FormatException("Wrong format of SSN");

            this._id = nid._id;
            this._type = NationalIdTypes.SSN;
        }

        public override bool IsValid()
        {
            return base.IsSSN ;
        }

    }

    /// <summary>
    /// Immutable class that represents swedish organisation number.
    /// Understands formats YYMMDD-CCCC and YYMMDDCCCC.
    /// Uses format YYMMDDCCCC when converting to string.
    /// </summary>
    public class OrganisationNumber: NationalId
    {
        public OrganisationNumber(string cin)
        {
            if (cin == null)
            {
                throw new ArgumentNullException(nameof(cin));
            }

            var nid = NationalId.Parse(cin);

            if (!nid.IsCIN)
                throw new FormatException("Wrong format of CIN");

            this._id = nid._id;
            this._type = NationalIdTypes.CIN;
        }

        public override bool IsValid()
        {
            return base.IsCIN;
        }
    }
}
