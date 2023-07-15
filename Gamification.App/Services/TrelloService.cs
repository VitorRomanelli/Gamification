using Gamification.App.Models;
using Gamification.Core.Entities;
using Gamification.Infra.Context;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static Gamification.App.Models.TrelloModels;

namespace Gamification.App.Services
{
    public class TrelloService
    {
        private readonly AppDbContext _db;

        public TrelloService(AppDbContext db)
        {
            _db = db;
        }

        //public async Task<ResponseModel> GetTrelloMoveAction(TrelloResponseModel model)
        //{
        //    try
        //    {
        //        switch(model.action.type)
        //        {
        //            case "createCard":
        //                {
        //                    var order = new Orde
        //                    {
        //                        Title = model.action.data.card.name,
        //                        Description = model.action.data.card.name,
        //                        RewardPoints = 100,
        //                        Step = Step.Schedule,
        //                    };

        //                    await _db.AddAsync(order);
        //                    await _db.SaveChangesAsync();

        //                    var log = new TrelloActionLog
        //                    {
        //                        TrelloPayload = JsonConvert.SerializeObject(model),
        //                    };

        //                    await _db.AddAsync(log);
        //                    await _db.SaveChangesAsync();

        //                    break;
        //                }
        //        }

        //        return ResponseModel.BuildOkResponse("Ação armazenada com sucesso!");
        //    }
        //    catch
        //    {
        //        return ResponseModel.BuildErrorResponse("Ocorreu um erro ao verificar ação do Trello");
        //    }
        //}
    }
}
