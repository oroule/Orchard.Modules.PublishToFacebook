using Orchard.Workflows.Services;
using Orchard.Workflows.Models;
using Orchard.Localization;
using Orchard.UI.Notify;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Orchard.Environment.Extensions;

namespace KRL.PublishToFacebook.Activities
{
    [OrchardFeature("KRL.PublishToFacebook")]
    public class PublishToFacebookActivity : Task {
        private readonly INotifier _notifier;

        public PublishToFacebookActivity(INotifier notifier)
        {
            _notifier = notifier;
            T = NullLocalizer.Instance;
        }

        public Localizer T { get; set; }

        public override string Name {
            get { return "PublishToFacebook"; }
        }

        public override LocalizedString Category {
            get { return T("Notification"); }
        }

        public override LocalizedString Description {
            get { return T("Publish message to facebook application");  }
        }

        public override string Form {
            get { return "ActivityNotify"; }
        }

        public override IEnumerable<LocalizedString> GetPossibleOutcomes(WorkflowContext workflowContext, ActivityContext activityContext) {
            yield return T("Done");
        }

        public override IEnumerable<LocalizedString> Execute(WorkflowContext workflowContext, ActivityContext activityContext) {
            var notification = activityContext.GetState<string>("Notification");
            var message = activityContext.GetState<string>("Message");

            NotifyType notificationType;
            Enum.TryParse(notification, true, out notificationType);
            _notifier.Add(notificationType, T(message));

            yield return T("Done");
        }
    }
}