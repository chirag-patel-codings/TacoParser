using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoggingKata
{

    // TocoBell class with it's attributes
    public class TacoBell : ITrackable
    {

        private string _name = "";
        private Point _location = new Point();

        public TacoBell(string name, Point location)
        {
            _name = name;
            _location = location;
        }

        public string Name 
        { 
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }

        public Point Location 
        { 
            get 
            {
                return _location;
            }
            set
            {
                _location = value;
            } 
        }   

    }
}
