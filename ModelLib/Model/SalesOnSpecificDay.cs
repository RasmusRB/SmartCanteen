using System;
using System.Collections.Generic;
using System.Text;

namespace ModelLib.Model
{
    public class SalesOnSpecificDay
    {
        private int _prodId;
        private int _salesQuantity;
        private string _prodName;
        private double _prodPrice;
        private double _totalPrice;
        private DateTime _orderDate;

        public SalesOnSpecificDay()
        {

        }

        public SalesOnSpecificDay(int prodId, int salesQuantity, string prodName, double prodPrice, double totalPrice, DateTime orderDate)
        {
            _prodId = prodId;
            _salesQuantity = salesQuantity;
            _prodName = prodName;
            _prodPrice = prodPrice;
            _totalPrice = totalPrice;
            _orderDate = orderDate;
        }

        public int ProdId
        {
            get => _prodId;
            set => _prodId = value;
        }

        public int SalesQuantity
        {
            get => _salesQuantity;
            set => _salesQuantity = value;
        }

        public string ProdName
        {
            get => _prodName;
            set => _prodName = value;
        }

        public double ProdPrice
        {
            get => _prodPrice;
            set => _prodPrice = value;
        }

        public double TotalPrice
        {
            get => _totalPrice;
            set => _totalPrice = value;
        }

        public DateTime OrderDate
        {
            get => _orderDate;
            set => _orderDate = value;
        }

        public override string ToString()
        {
            return $"{nameof(_prodId)}: {_prodId}, {nameof(_salesQuantity)}: {_salesQuantity}, {nameof(_prodName)}: {_prodName}, {nameof(_prodPrice)}: {_prodPrice}, {nameof(_totalPrice)}: {_totalPrice}, {nameof(_orderDate)}: {_orderDate}";
        }
    }
}
