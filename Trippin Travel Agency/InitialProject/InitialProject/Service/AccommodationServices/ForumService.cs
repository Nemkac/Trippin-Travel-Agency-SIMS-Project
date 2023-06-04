using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Metrics;
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

        public List<ForumComment> GetForumsComments(Forum forum)
        {
            DataBaseContext context = new DataBaseContext();
            List<ForumComment> comments = context.ForumComments.ToList();
            List<ForumComment> foundComments = new List<ForumComment>();
            foreach(ForumComment comment in comments)
            {
                if(comment.forumId == forum.id)
                {
                    foundComments.Add(comment);
                }
            }
            return foundComments;
        }

        public void AddComment(ForumComment comment)
        {
            DataBaseContext context = new DataBaseContext();
            context.Add(comment);
            context.SaveChanges();
        }

        public List<Forum> GetAllByCountry(string country)
        {
            if (country != null && country != string.Empty)
            {
                DataBaseContext context = new DataBaseContext();
                List<AccommodationLocation> locations = context.AccommodationLocation.ToList();
                List<Forum> forums = context.Forums.ToList();
                List<Forum> foundForums = new List<Forum>();
                foreach(Forum forum in forums)
                {
                    if(GetLocation(forum.id)[0].ToUpper().Contains(country.ToUpper()))
                    {
                        foundForums.Add(forum);
                    }
                }
                return foundForums;
            }
            return null;
        }

        public List<Forum> GetAllByCity(string city)
        {
            if (city != null && city != string.Empty)
            {
                DataBaseContext context = new DataBaseContext();
                List<AccommodationLocation> locations = context.AccommodationLocation.ToList();
                List<Forum> forums = context.Forums.ToList();
                List<Forum> foundForums = new List<Forum>();
                foreach (Forum forum in forums)
                {
                    if (GetLocation(forum.id)[1].ToUpper().Contains(city.ToUpper()))
                    {
                        foundForums.Add(forum);
                    }
                }
                return foundForums;
            }
            return null;
        }

        public List<Forum> GetMathching(List<Forum> forums1, List<Forum> forums2)
        {
            if(forums2 == null)
            {
                return forums1;
            }
            if(forums1 == null)
            {
                return forums2;
            } 
            List<Forum> matchingForums = new List<Forum>();
            foreach(Forum forum1 in forums1)
            {
                foreach(Forum forum2 in forums2)
                {
                    if(forum1.id == forum2.id)
                    {
                        matchingForums.Add(forum1);
                    }
                }
            }
            return matchingForums;
        }
    }
}
