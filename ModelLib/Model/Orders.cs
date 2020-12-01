using System;
using System.Collections.Generic;
using System.Text;

namespace ModelLib.Model
{
    public class Orders
    {
        private int _orderId;
        private int _productCount;
        private DateTime _orderDate;

        public Orders()
        {
        }

        public Orders(int orderId, int productCount, DateTime orderDate)
        {
            _orderId = orderId;
            _productCount = productCount;
            _orderDate = orderDate;
        }

        public int OrderId
        {
            get => _orderId;
            set => _orderId = value;
        }

        public int ProductCount
        {
            get => _productCount;
            set => _productCount = value;
        }

        public DateTime OrderDate
        {
            get => _orderDate;
            set => _orderDate = value;
        }

        public override string ToString()
        {
            return $"{nameof(_orderId)}: {_orderId}, {nameof(_productCount)}: {_productCount}, {nameof(_orderDate)}: {_orderDate}";
        }
    }
}
