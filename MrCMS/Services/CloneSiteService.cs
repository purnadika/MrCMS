using MrCMS.DbConfiguration;
using MrCMS.Entities.Multisite;
using MrCMS.Models;
using NHibernate;

namespace MrCMS.Services
{
    public class CloneSiteService : ICloneSiteService
    {
        private readonly ICloneSitePartsService _cloneSitePartsService;
        private readonly ISession _session;

        public CloneSiteService(ICloneSitePartsService cloneSitePartsService, ISession session)
        {
            _cloneSitePartsService = cloneSitePartsService;
            _session = session;
        }

        public void CloneData(Site site, SiteCopyOptions options)
        {
            using (new SiteFilterDisabler(_session))
            {
                if (!options.SiteId.HasValue)
                    return;

                var @from = _session.Get<Site>(options.SiteId.Value);
                if (@from == null)
                    return;
                _cloneSitePartsService.CopySettings(@from, site);
                if (options.CopyLayouts)
                    _cloneSitePartsService.CopyLayouts(@from, site);
                if (options.CopyMediaCategories)
                    _cloneSitePartsService.CopyMediaCategories(@from, site);
                if (options.CopyHome)
                    _cloneSitePartsService.CopyHome(@from, site);
                if (options.Copy404)
                    _cloneSitePartsService.Copy404(@from, site);
                if (options.Copy403)
                    _cloneSitePartsService.Copy403(@from, site);
                if (options.Copy500)
                    _cloneSitePartsService.Copy500(@from, site);
                if (options.CopyLogin)
                    _cloneSitePartsService.CopyLogin(@from, site);
            }
        }
    }
}