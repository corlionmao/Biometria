using BiometriaAPI.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BiometriaAPI.Controllers
{
    public class FingerprintController : ApiController
    {
        // GET: api/Fingerprint
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        // GET: api/Fingerprint/5
        [Route("api/fingerprint/{numint}")]
        public ResponseModel Get(string numint)
        {
            ResponseModel _objResponseModel = new ResponseModel();
            Fingerprint huella = new Fingerprint();
            try
            {
                AccesoDatosSQL.AccesoDatosSQL cnx = new AccesoDatosSQL.AccesoDatosSQL(RecursosNavegador.Recursos.GetConfiguration("conexionSQL"));
                DataTable dt = cnx.EjecutarConsultaDT("Select [Numint],[Dedo],[IsoTemplate],[ImagenDedo],[Creado],[Modificado],[Mano] FROM[dbo].[Huella] WHERE Numint ='" + numint + "'");
                foreach (DataRow row in dt.Rows)
                {

                    huella.NUMINT = row["Numint"].ToString();
                    huella.IsoTemplate = row["IsoTemplate"].ToString();
                    huella.ImagenDedo = row["ImagenDedo"].ToString();
                    huella.Mano = Convert.ToInt16(row["Dedo"]);
                    huella.Mano = Convert.ToInt16(row["Mano"]);
                    huella.Creado = Convert.ToDateTime(row["Creado"]);
                    huella.Modificado = Convert.ToDateTime(row["Modificado"]);

                }
                _objResponseModel.Data = huella;
                _objResponseModel.Status = true;
                _objResponseModel.Message = "Data Received successfully";

            }
            catch (Exception ex)
            {
                _objResponseModel.Data = huella;
                _objResponseModel.Status = false;
                _objResponseModel.Message = "Data Failed to receive";
                throw new Exception("Error : " + ex.Message);
            }
            return _objResponseModel;
        }



        // POST: api/Fingerprint
        [HttpPost]
        public IHttpActionResult Post(Fingerprint huella)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                AccesoDatosSQL.AccesoDatosSQL cnx = new AccesoDatosSQL.AccesoDatosSQL(RecursosNavegador.Recursos.GetConfiguration("conexionSQL"));
                string formatoFecha = "yyyy-MM-dd HH:mm:ss";
                cnx.EjecutaQryRegsAct("INSERT INTO [Huella] ([Numint],[Dedo],[IsoTemplate],[ImagenDedo],[Creado],[Mano] ) VALUES" +
                           String.Format("('{0}',{1},'{2}','{3}','{4}',{5})", huella.NUMINT, huella.Dedo, 
                           huella.IsoTemplate, huella.ImagenDedo, DateTime.Now.ToString(formatoFecha),huella.Mano));

            }
            catch (Exception ex)
            {
                throw new Exception("Error : " + ex.Message);
            }
            return CreatedAtRoute("DefaultApi", new { id = huella.NUMINT }, huella);
        }

        // PUT: api/Fingerprint/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Fingerprint/5
        public void Delete(int id)
        {
        }
    }
}
