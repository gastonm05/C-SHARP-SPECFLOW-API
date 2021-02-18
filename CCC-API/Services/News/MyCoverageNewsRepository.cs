using System;
using System.Collections.Generic;
using System.Linq;
using CCC_API.Data.Responses.News;

namespace CCC_API.Services.News
{
    /// <summary>
    /// Register created news & group by different functions.
    /// </summary>
    public class MyCoverageNewsRepository
    {
        private List<NewsItem> CreatedNews { get; } = new List<NewsItem>();

        public MyCoverageNewsRepository()
        {
            CreatedNews = new List<NewsItem>();
        }

        /// <summary>
        /// Register created news.
        /// </summary>
        /// <param name="newsItem"></param>
        public void RegisterNews(NewsItem newsItem)
        {
            CreatedNews.Add(newsItem);
        }
        
        /// <summary>
        /// Provides created news for date.
        /// </summary>
        /// <param name="groupingByFunc">How to group news</param>
        /// <param name="filterPredicate">What news expected, facet</param>
        /// <returns></returns>
        public IEnumerable<Tuple<DateTime, List<NewsItem>>> GetCreatedNewsItemsGroupedBy(
            Func<DateTime, DateTime> groupingByFunc, Predicate<NewsItem> filterPredicate)
        {
            var expTonedNewsByDate =
                from news in CreatedNews
                where filterPredicate(news)
                group news by groupingByFunc(news.NewsDate.Date)
                into newsItemsForDate
                select new Tuple<DateTime, List<NewsItem>>(newsItemsForDate.Key, newsItemsForDate.ToList());
            return expTonedNewsByDate;
        }
    }
}
