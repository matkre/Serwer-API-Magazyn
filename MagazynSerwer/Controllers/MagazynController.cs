using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MagazynBazaProj;

namespace MagazynSerwer.Controllers
{
    public class MagazynController : ApiController
    {
            public IEnumerable<Magazyn> Get()
            {
                bazaEntities entities = new bazaEntities();
                return entities.Magazyns.ToList();
            }

            public Magazyn Get(int id)
            {
                bazaEntities entities = new bazaEntities();
                return entities.Magazyns.FirstOrDefault(e => e.ID == id);
            }

            public HttpResponseMessage Create_Product([FromBody] Magazyn m)
            {
                try
                {
                    bazaEntities entities = new bazaEntities();
                    entities.Magazyns.Add(m);
                    entities.SaveChanges();
                    var message = Request.CreateResponse(HttpStatusCode.Created, m);
                    message.Headers.Location = new Uri(Request.RequestUri + m.ID.ToString());
                    return message;
                }
                catch (Exception ex)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
                }
            }

            public IHttpActionResult Delete_Product(int id)
            {
                bazaEntities db = new bazaEntities();
                Magazyn product = db.Magazyns.Find(id);
                if (product == null)
                {
                    return NotFound();
                }
                db.Magazyns.Remove(product);
                db.SaveChanges();
                return Ok(product);
            }

        [HttpPut]
        public HttpResponseMessage Update_Product(int id, [FromBody] Magazyn m)
            {
                try
                {
                    bazaEntities entities = new bazaEntities();
                    var dbe = entities.Magazyns.FirstOrDefault(e => e.ID == id);
                    if (dbe == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Brak produktu o ID = "+id.ToString());
                    }
                    else
                    {
                        dbe.ID = m.ID;
                        dbe.Nazwa = m.Nazwa;
                        dbe.Kategoria = m.Kategoria;
                        dbe.Producent = m.Producent;
                        dbe.Ilość = m.Ilość;
                        dbe.Samochód = m.Samochód;
                        dbe.Silnik = m.Silnik;
                        dbe.Cena = m.Cena;
                        entities.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK, dbe);
                    }
                }
                catch (Exception ex)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
                }
            }
        }
    }
