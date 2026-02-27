using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsharpControlWork1
{
    class Edition
    {
        protected string _title;
        protected DateTime _releaseData;
        protected int _pressrun;

        public string Title
        {
            get
            {
                return _title;
            }
            set
            {
                _title = value;
            }
        }
        public DateTime ReleaseData
        {
            get
            {
                return _releaseData;
            }
            set
            {
                _releaseData = value;
            }
        }

        public int PressRun
        {
            get
            {
                return _pressrun;
            }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException("НЕДОПУСТИМО! Тираж моего издания может лететь только в положительную бесконечность");
                }
                _pressrun = value;
            }
        }



        public Edition(string edition, DateTime releaseDataEdition, int pressrun)
        {
            _title = edition;
            _releaseData = releaseDataEdition;
            _pressrun = pressrun;
        }

        public Edition() 
        {
            _title = default(string);
            _releaseData= default(DateTime);
            _pressrun = default(int);
        }

        public virtual object DeepCopy()

        {
            Edition editionCopy = new Edition(_title, _releaseData, _pressrun);
            return editionCopy;
        }

        public override bool Equals(object? obj)
        {
            if (obj is null || !(obj is Edition))
            { 
                throw new Exception("Не тот тип");
            }   

            Edition other = (Edition)obj;

            return _title == other._title &&
                _releaseData == other._releaseData &&
                _pressrun == other._pressrun;
        }

        public static bool operator ==(Edition edition1, Edition edition2)
        {
            return Equals(edition1, edition2);
        }

        public static bool operator !=(Edition edition1, Edition edition2)
        {
            return !Equals(edition1, edition2);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_title, _releaseData, _pressrun);
        }

        public override string ToString()
        {
            return $"Название издания - {_title}\n" +
                $"Дата выпуска - {_releaseData}\n" +
                $"Тираж - {_pressrun}\n";
        }
    }
}
