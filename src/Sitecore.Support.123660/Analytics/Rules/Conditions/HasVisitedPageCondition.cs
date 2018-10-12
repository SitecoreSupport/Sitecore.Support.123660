namespace Sitecore.Support.Analytics.Rules.Conditions
{
  using System;
  using System.Linq;
  using Sitecore.Analytics;
  using Sitecore.Diagnostics;
  using Sitecore.Rules;
  using Sitecore.Rules.Conditions;

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

      return Tracker.Current.Session.Interaction.GetPages().Any(row => row.Item.Id == pageGuid);
    }

    #endregion
  }
}