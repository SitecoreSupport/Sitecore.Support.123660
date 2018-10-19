namespace Sitecore.Support.Analytics.Rules.Conditions
{
  using System;
  using System.Linq;
  using Sitecore.Analytics;
  using Sitecore.Diagnostics;
  using Sitecore.Rules;
  using Sitecore.Rules.Conditions;
  using System.Collections.Generic;
  using System.Reflection;
  using Sitecore.Analytics.Model;
  using Sitecore.Analytics.Tracking;


  /// <summary>Defines the when subitem of class.</summary>
  /// <typeparam name="T">The rule context.</typeparam>
  public class HasVisitedPageCondition<T> : WhenCondition<T> where T : RuleContext
  {
    #region Properties

    /// <summary>
    /// Gets or sets the value.
    /// </summary>
    /// <value>The value.</value>
    [NotNull]
    public string PageId { get; set; }

    #endregion

    #region Methods

    /// <summary>Executes the specified rule context.</summary>
    /// <param name="ruleContext">The rule context.</param>
    /// <returns><c>True</c>, if the condition succeeds, otherwise <c>false</c>.</returns>
    protected override bool Execute([NotNull] T ruleContext)
    {
      Assert.ArgumentNotNull(ruleContext, "ruleContext");
      Assert.IsNotNull(Tracker.Current, "Tracker.Current is not initialized");
      Assert.IsNotNull(Tracker.Current.Session, "Tracker.Current.Session is not initialized");
      Assert.IsNotNull(Tracker.Current.Session.Interaction, "Tracker.Current.Session.Interaction is not initialized");

      Guid pageGuid;
      try
      {
        pageGuid = new Guid(this.PageId);
      }
      catch
      {
        Log.Warn(string.Format("Could not convert value to guid: {0}", this.PageId), this.GetType());
        return false;
      }

      return GetPages(Tracker.Current.Session.Interaction).Any(page => page.Item.Id == pageGuid);
    }

    // Sitecore.Support.Analytics.Rules.Conditions.HasVisitedPageCondition<T>
    public IEnumerable<PageData> GetPages(CurrentInteraction interaction)
    {
      return
        this.GetVisitData(interaction).Pages; //.ConvertAll<AggregationPageContext>((PageData p) => new AggregationPageContext(p));
    }

    // Sitecore.Support.Analytics.Rules.Conditions.HasVisitedPageCondition<T>
    private VisitData GetVisitData(CurrentInteraction interaction)
    {
      Type type = interaction.GetType();
      PropertyInfo propertyInfo = null;
      while (propertyInfo == null)
      {
        propertyInfo = type.GetProperty("VisitData", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
        type = type.BaseType;
      }
      return (VisitData)propertyInfo.GetValue(interaction);
    }


    #endregion
  }
}