using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
	public class BasePaging
	{
        public void GetData(IQueryable<object> data,int pageId , int take, int showPageCount)
        {
            PageCount = data.Count() / take;
            if (data.Count() % take > 0) PageCount++;
            if (pageId < 1) pageId = 1;
            if (pageId > PageCount) pageId = PageCount;
            PageId = pageId;
            Take = take;
            DataCount = data.Count();
            if(take < 1) take = 10;
            Take = take;
            Skip = (pageId - 1) * take;
            if(showPageCount < 1) showPageCount = 2;
            StartPage = pageId > showPageCount ? pageId - showPageCount : 1 ;
            EndPage = PageCount - pageId > showPageCount ? pageId + showPageCount : PageCount ; 
        }
        public int PageId { get; private set; }
        public int PageCount { get; private set; }
        public int Take { get; private set; }
        public int DataCount { get; private set; }
        public int Skip { get; private set; }
        public int StartPage { get; private set; }
        public int EndPage { get; private set; }
	}
}
