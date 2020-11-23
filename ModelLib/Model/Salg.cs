using System;
using System.Collections.Generic;
using System.Text;

namespace ModelLib.Model
{
    public class Salg
    {
        private string _drikkeVare;
        private string _mad;

        public Salg()
        {

        }

        public Salg(string drikkeVare, string mad)
        {
            _drikkeVare = drikkeVare;
            _mad = mad;
        }

        public string DrikkeVare
        {
            get => _drikkeVare;
            set => _drikkeVare = value;
        }

        public string Mad
        {
            get => _mad;
            set => _mad = value;
        }

        public override string ToString()
        {
            return $"{nameof(_drikkeVare)}: {_drikkeVare}, {nameof(_mad)}: {_mad}";
        }
    }
}
