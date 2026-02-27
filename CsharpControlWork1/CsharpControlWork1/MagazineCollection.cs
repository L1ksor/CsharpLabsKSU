using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsharpControlWork1
{
    delegate TKey KeySelector<TKey>(Magazine mg);

    class MagazineCollection<Tkey> 
    {
        Dictionary<Tkey, Magazine> _dictMagazine = new();
        
        KeySelector<Tkey> _keySelector;

        public MagazineCollection(KeySelector<Tkey> keySelector)
        {
            _keySelector = keySelector;
        }

        public void AddDefaults()
        {
            _dictMagazine.Add(new Magazine());
        }

        public void AddMagazines(params Magazine[] magazines) { } 

        public override string ToString() { }

        public string ToShortString() { } 
    }
}
}
