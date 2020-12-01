using System;
using System.Collections.Generic;
using System.Text;

namespace ModelLib.Model
{
    public class Products
    {
        private int _prodId;
        private int _fkId;
        private string _name;
        private int _price;
        private int _protein;
        private bool _isHot;

        public Products()
        {

        }

        public Products(int prodId, int fkId, string name, int price, int protein, bool isHot)
        {
            _prodId = prodId;
            _fkId = fkId;
            _name = name;
            _price = price;
            _protein = protein;
            _isHot = isHot;
        }

        public int ProdId
        {
            get => _prodId;
            set => _prodId = value;
        }

        public int FkId
        {
            get => _fkId;
            set => _fkId = value;
        }

        public string Name
        {
            get => _name;
            set => _name = value;
        }

        public int Price
        {
            get => _price;
            set => _price = value;
        }

        public int Protein
        {
            get => _protein;
            set => _protein = value;
        }

        public bool IsHot
        {
            get => _isHot;
            set => _isHot = value;
        }

        public override string ToString()
        {
            return $"{nameof(_prodId)}: {_prodId}, {nameof(_fkId)}: {_fkId}, {nameof(_name)}: {_name}, {nameof(_price)}: {_price}, {nameof(_protein)}: {_protein}, {nameof(_isHot)}: {_isHot}";
        }
    }
}
