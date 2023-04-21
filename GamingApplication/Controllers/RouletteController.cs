using GamingApplication.Interfaces;
using GamingApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace GamingApplication.Controllers
{
    public class RouletteController : ApiController
    {
        private readonly IGamingApplication _gamingFunction;


        [System.Web.Http.Route("api/game/spin")]
        [System.Web.Http.HttpPost]
        public async Task<HttpResponseMessage> Spin([FromBody] Request Req)
        {
            try
            {
                Random ran = new Random();
                var r = new Random();
                string[] color = { "Red", "Black" };
                string error = "";
                int attempts = 0;
                int money = 500;

                List<Response> lstresponse = new List<Response>();
                while (attempts >= Req.no_spin)
                {
                    while (money != 0)
                    {
                        Console.WriteLine("Roulette Roller by Alifyandra\n");
                        Console.WriteLine("Money:$" + money + "                  Attempts: " + attempts);
                        Console.WriteLine("Type in any off the following letters below:");
                        Console.WriteLine("a.Even    b.Odd    c.1 to 18    d.19 to 36");
                        Console.WriteLine("e.Red     f.Black  g.1st 12     h.2nd 12");
                        Console.WriteLine("i.3rd 12");

                        //guess verifier
                        bool check = Req.guess == "a" || Req.guess == "b" || Req.guess == "c" || Req.guess == "d" || Req.guess == "e" || Req.guess == "f" || Req.guess == "g" || Req.guess == "h" || Req.guess == "i";
                        if (check == false)
                        {
                            throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Guess is not Correct"));
                        }
                        else
                        {
                            //bet verifier
                            if (Req.bet > money)
                            {
                                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "There is no enough money for bet"));
                            }
                            else
                            {
                                money -= Req.bet;
                                int roll = ran.Next(0, 37);
                                string ranColor = color[r.Next(color.Length)];
                                bool even = roll % 2 == 0;
                                Response rsp = new Response();
                                if ((((Req.guess == "a") && (even == true))) || (((Req.guess == "b") && (even == false))) || ((Req.guess == "e") && (ranColor == "Red") || (Req.guess == "f") && (ranColor == "Black")))
                                {
                                    rsp.rou_rolled = ranColor + " " + roll;
                                    rsp.winning_money = Req.bet * 2;
                                    money += Req.bet * 2;
                                    attempts += 1;
                                }
                                else if ((Req.guess == "c") && ((roll > 0) && (roll < 19)))
                                {
                                    rsp.rou_rolled = ranColor + " " + roll;
                                    rsp.winning_money = Req.bet * 2;
                                    money += Req.bet * 2;
                                    attempts += 1;
                                }
                                else if ((Req.guess == "d") && ((roll > 18) && (roll < 37)))
                                {
                                    rsp.rou_rolled = ranColor + " " + roll;
                                    rsp.winning_money = Req.bet * 2;
                                    money += Req.bet * 2;
                                    attempts += 1;
                                    Console.ReadKey();
                                }
                                else if ((Req.guess == "g") && (roll > 0 && roll < 13) || (Req.guess == "h") && (roll > 12 && roll < 25) || (Req.guess == "i") && (roll > 24 && roll < 37))
                                {
                                    rsp.rou_rolled = ranColor + " " + roll;
                                    rsp.winning_money = Req.bet * 3;
                                    money += Req.bet * 3;
                                    attempts += 1;
                                    Console.ReadKey();
                                }
                                else
                                {
                                    rsp.rou_rolled = ranColor + " " + roll;
                                    rsp.winning_money = Req.bet;
                                    attempts += 1;
                                }
                                lstresponse.Add(rsp);
                                _gamingFunction.InsertGamingData(rsp.rou_rolled, rsp.winning_money, Req.player, ref error);
                            }                           
                        }
                    }
                }
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                return Request.CreateResponse(HttpStatusCode.OK, lstresponse, Configuration.Formatters.JsonFormatter);
            }
            catch(Exception e)
            {
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e.Message));
            }
        }
    }
}