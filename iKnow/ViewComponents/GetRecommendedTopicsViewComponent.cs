using System;
using System.Collections.Generic;
using System.Linq;
using iKnow.Core;
using iKnow.Core.Models;
using iKnow.Core.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace iKnow.ViewComponents
{
    public class GetRecommendedTopicsViewComponent : ViewComponent
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetRecommendedTopicsViewComponent(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IViewComponentResult Invoke(int? id)
        {
            IEnumerable<Topic> topics = id == null
                ? _unitOfWork.TopicRepository.GetAll(q => q.OrderBy(t => Guid.NewGuid()), null, null,
                    Constants.RecommendedTopicNumber).ToList()
                : _unitOfWork.TopicRepository.Get(t => t.Id != id.Value, q => q.OrderBy(t => Guid.NewGuid()), null, null,
                    Constants.RecommendedTopicNumber).ToList();

            return View(topics);
        }
    }
}