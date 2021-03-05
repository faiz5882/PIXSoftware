using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIMCORE_TOOL.com.crispico.BHS_Analysis
{
    public class AnalysisResultsDisplayParameters
    {        
        public int maxNbRecirculations { get; set; }
        public List<int> segregationLineNumbers = new List<int>();

        public AnalysisResultsDisplayParameters(int _maxRecirc, List<int> _segregationLineNumbers)
        {            
            maxNbRecirculations = _maxRecirc;
            segregationLineNumbers.AddRange(_segregationLineNumbers);
        }

        public void update(int pMaxRecirc, List<int> pSegregationLineNumbers)
        {
            if (maxNbRecirculations < pMaxRecirc)
                maxNbRecirculations = pMaxRecirc;
            foreach (int line in pSegregationLineNumbers)
            {
                if (!segregationLineNumbers.Contains(line))
                    segregationLineNumbers.Add(line);
            }
            segregationLineNumbers.Sort();
        }
    }
}
