using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitialProject.Context;
using InitialProject.Model;


namespace InitialProject.Service.AccommodationServices
{
    public class ForumService
    {
        public List<Forum> GetAll()
        {
            DataBaseContext context = new DataBaseContext();
            List<Forum> forums = context.Forums.ToList();
            return forums;
        }

        public Forum GetById(int forumId)
        {
            DataBaseContext context = new DataBaseContext();
            List<Forum> forums = context.Forums.ToList();
            foreach (Forum forum in forums)
            {
                if(forum.id == forumId)
                {
                    return forum;
                }
            }
            return null;
        }

        public List<Forum> GetByCreatorId()
        {
            DataBaseContext context = new DataBaseContext();
            List<Forum> forums = context.Forums.ToList();
            List<Forum> foundForums = new List<Forum>();
            foreach (Forum forum in forums)
            {
                if (forum.creatorId == LoggedUser.id)
                {
                    foundForums.Add(forum);
                }
            }
            return foundForums;
        }

        public List<string> GetLocation(int forumId)
        {
            DataBaseContext context = new DataBaseContext();
            List<Forum> forums = context.Forums.ToList();
            List<AccommodationLocation> locations = context.AccommodationLocation.ToList();
            Forum forum = forums.Find(f => f.id == forumId);
            return new List<string>() { forum.location.country, forum.location.city };
        }
    }
}
