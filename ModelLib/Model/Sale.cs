using System;
using System.Collections.Generic;
using System.Text;

namespace ModelLib.Model
{
    public class Sale
    {
        private string _drinks;
        private string _food;

        public Sale()
        {

        }

        public Sale(string drinks, string food)
        {
            _drinks = drinks;
            _food = food;
        }

        public string Drinks
        {
            get => _drinks;
            set => _drinks = value;
        }

        public string Food
        {
            get => _food;
            set => _food = value;
        }

        public override string ToString()
        {
            return $"{nameof(_drinks)}: {_drinks}, {nameof(_food)}: {_food}";
        }
    }
}
