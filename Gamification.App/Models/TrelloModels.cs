using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gamification.App.Models
{
    public class TrelloModels
    {
        public class TrelloResponseModel
        {
            public Action action { get; set; }
            public Model model { get; set; }
            public Webhook webhook { get; set; }
        }

        public class Action
        {
            public string id { get; set; }
            public string idMemberCreator { get; set; }
            public Data data { get; set; }
            public string type { get; set; }
            public DateTime date { get; set; }
            public Membercreator memberCreator { get; set; }
        }

        public class Data
        {
            public Board board { get; set; }
            public Card card { get; set; }
            public bool voted { get; set; }
        }

        public class Board
        {
            public string name { get; set; }
            public string id { get; set; }
        }

        public class Card
        {
            public int idShort { get; set; }
            public string name { get; set; }
            public string id { get; set; }
        }

        public class Membercreator
        {
            public string id { get; set; }
            public string avatarHash { get; set; }
            public string fullName { get; set; }
            public string initials { get; set; }
            public string username { get; set; }
        }

        public class Model
        {
            public string id { get; set; }
            public string name { get; set; }
            public string desc { get; set; }
            public bool closed { get; set; }
            public string idOrganization { get; set; }
            public bool pinned { get; set; }
            public string url { get; set; }
            public Prefs prefs { get; set; }
            public Labelnames labelNames { get; set; }
        }

        public class Prefs
        {
            public string permissionLevel { get; set; }
            public string voting { get; set; }
            public string comments { get; set; }
            public string invitations { get; set; }
            public bool selfJoin { get; set; }
            public bool cardCovers { get; set; }
            public bool canBePublic { get; set; }
            public bool canBeOrg { get; set; }
            public bool canBePrivate { get; set; }
            public bool canInvite { get; set; }
        }

        public class Labelnames
        {
            public string yellow { get; set; }
            public string red { get; set; }
            public string purple { get; set; }
            public string orange { get; set; }
            public string green { get; set; }
            public string blue { get; set; }
        }

        public class Webhook
        {
            public string id { get; set; }
            public string description { get; set; }
            public string idModel { get; set; }
            public string callbackURL { get; set; }
            public bool active { get; set; }
            public int consecutiveFailures { get; set; }
            public string firstConsecutiveFailDate { get; set; }
        }

    }
}
