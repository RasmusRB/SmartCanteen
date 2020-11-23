using System;
using System.Collections.Generic;
using System.Text;

namespace ModelLib.Model
{
    public class Customers
    {
        private int _counter;
        private DateTime _dateTime;

        public Customers()
        {

        }

        public Customers(int counter, DateTime dateTime)
        {
            _counter = counter;
            _dateTime = dateTime;
        }

        public int Counter
        {
            get => _counter;
            set => _counter = value;
        }

        public DateTime DateTime
        {
            get => _dateTime;
            set => _dateTime = value;
        }

        public override string ToString()
        {
            return $"{nameof(_counter)}: {_counter}, {nameof(_dateTime)}: {_dateTime}";
        }
    }
}
