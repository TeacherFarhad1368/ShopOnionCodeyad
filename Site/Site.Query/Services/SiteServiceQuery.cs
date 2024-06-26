﻿using Shared.Application;
using Site.Application.Contract.SiteServiceApplication.Query;
using Site.Domain.SiteServiceAgg;

namespace Site.Query.Services;

internal class SiteServiceQuery : ISiteServiceQuery
{
	private readonly ISiteServiceRepository _siteServiceRepository;

	public SiteServiceQuery(ISiteServiceRepository siteServiceRepository)
	{
		_siteServiceRepository = siteServiceRepository;
	}

	public List<SiteServiceAdminQueryModel> GetAllForAdmin() =>
		_siteServiceRepository.GetAllQuery()
		.Select(s => new SiteServiceAdminQueryModel
		(s.Id,s.Title,FileDirectories.ServiceImageDirectory100 + s.ImageName,s.ImageAlt,s.CreateDate.ToPersainDate(),s.Active)).ToList();

    public List<SiteServiceUIQueryModel> GetAllForUI() =>
        _siteServiceRepository.GetAllByQuery(s=>s.Active)
        .Select(s => new SiteServiceUIQueryModel
        (s.Id, s.Title, FileDirectories.ServiceImageDirectory + s.ImageName, s.ImageAlt)).ToList();
}
