namespace Sitecore.Support.ExperienceAnalytics.Aggregation.Rules.AggregationAdaptor
{
  using System;
  using System.Collections.Generic;
  using Sitecore.Analytics.Data.Items;
  using Sitecore.Analytics.Model;
  using Sitecore.Analytics.Tracking;

  public class AggregationPageContext : IPageContext
  {
    public MvTestData MvTest { get; set; }

    public DateTime DateTime { get; set; }

    public int Duration { get; set; }

    public ItemData Item { get; set; }

    public IEnumerable<Sitecore.Analytics.Model.PageEventData> PageEvents { get; private set; }

    public SitecoreDeviceData SitecoreDevice { get; set; }

    public UrlData Url { get; set; }

    public int VisitPageIndex { get; set; }

    public bool CanSetDuration { get; private set; }

    public Session Session { get; private set; }

    public AggregationPageContext(PageData pageData)
    {
      this.Item = pageData.Item;
    }

    public bool IsTestSetIdNull()
    {
      return false;
    }


    public Sitecore.Analytics.Model.PageEventData Register(string name, string text)
    {
      return null;
    }

    public Sitecore.Analytics.Model.PageEventData Register(PageEventItem pageEventItem)
    {
      return null;
    }

    public void SetUrl(string url)
    {
    }

    public Sitecore.Analytics.Model.PageEventData TriggerCampaign(CampaignItem campaignItem)
    {
      return null;
    }

    public void SetItemProperties(Guid itemId, string itemLanguage, int itemVersion)
    {
    }

    public Sitecore.Analytics.Model.PageEventData Register(Sitecore.Analytics.Data.PageEventData pageData)
    {
      throw new NotImplementedException();
    }

    public IEnumerable<Sitecore.Analytics.Model.PageEventData> RegisterEvents(IEnumerable<Sitecore.Analytics.Data.PageEventData> pageEvents)
    {
      throw new NotImplementedException();
    }
  }
}
