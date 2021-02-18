using System;
using System.Collections.Generic;
using TechTalk.SpecFlow;

namespace CCC_API.Steps
{
    [Binding]
    public class Transforms
    {

        /// <summary>
        /// Transforms a step with the data 'facet categories facet1, facet2, facet3' into a list
        /// IMPORTANT - This will strip out whitespace after commas (,)
        /// </summary>
        /// <param name="categories">The categories.</param>
        /// <returns></returns>
        [StepArgumentTransformation(@"facet categories (.*)")]
        public List<string> TransformMultipleFacetCategories(string categories)
        {
            var cats = categories.Split(new string[] { ", " }, StringSplitOptions.None);
            List<string> catList = new List<string>();
            catList.AddRange(cats);
            return catList;
        }
    }
}
