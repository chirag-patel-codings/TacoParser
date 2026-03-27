using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoggingKata
{
    // 'Class': Reference Type.
    // TocoBell class (Blue-Print) with it's attributes that implements 'ITrackable' interface.
    public class TacoBell : ITrackable
    {
        
        // Fields/Private members
        private string _name = "";
        private Point _location = new Point();

        // Constructor
        public TacoBell(string name, Point location)
        {
            _name = name;
            _location = location;
        }

        // Properties ('ITrackable' stubbed-out member) implementation
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

        // Properties ('ITrackable' stubbed-out member) implementation
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
