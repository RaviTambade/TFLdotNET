using BolgPost.Entities;
using BolgPost.DBContext;
using BolgPost.Repositories;

using (var context = new BlogContext())
{
    BlogRepository repo = new BlogRepository(context);
    repo.Initialize();
    repo.ShowAll();
}

