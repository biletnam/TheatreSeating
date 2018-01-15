using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheatreSeating.Models
{
    public class Row
    {
        private int rowNum;

        private List<Section> sections;

        public Row(int rowNum, string[] sections)
        {
            this.rowNum = rowNum;
            int secCount = 1;
            this.sections = new List<Section>();

            foreach (string sec in sections)
            {
                this.sections.Add(new Section(secCount,int.Parse(sec)));
                secCount += 1;
            }
        }

        public string CheckAvailability(int reqSeats)
        {
            
            string output = string.Empty;

            foreach (Section sec in sections)
            {
                if (sec.sectionSeatCount == reqSeats)
                {
                    sec.sectionSeatCount -= reqSeats;
                    return " Row "+this.rowNum.ToString()+" Section "+sec.sectionNum.ToString()+"\n";
                }
            }

            return output;
        }
    }

    class Section
    {
        public int sectionNum;

        public int sectionSeatCount;

        public Section(int sectionNum, int sectionSeatCount)
        {
            this.sectionNum = sectionNum;
            this.sectionSeatCount = sectionSeatCount;
        }
    }
}
