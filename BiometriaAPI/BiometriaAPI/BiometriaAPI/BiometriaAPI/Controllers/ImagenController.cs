using BiometriaAPI.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace BiometriaAPI.Controllers
{
    [EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*")]
    public class ImagenController : ApiController
    {

        [Route("api/imagen")]
        [HttpPost]
        public IHttpActionResult Post(Imagen imagen)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            try
            {
                AccesoDatosSQL.AccesoDatosSQL cnx = new AccesoDatosSQL.AccesoDatosSQL(RecursosNavegador.Recursos.GetConfiguration("conexionSQL"));
                string formatoFecha = "yyyy-MM-dd HH:mm:ss";
                cnx.EjecutaQryRegsAct("INSERT INTO [Imagen] ([numint],[tipo],[ImagenDataUrl],[ImagenAsBase64],[Creado] ) VALUES" +
                           String.Format("('{0}',{1},'{2}','{3}','{4}')", imagen.numint, imagen.tipo,
                           imagen.imagenDataUrl, imagen.imagenAsBase64, DateTime.Now.ToString(formatoFecha)));

            }
            catch (Exception ex)
            {
                throw new Exception("Error : " + ex.Message);
            }
            return CreatedAtRoute("DefaultApi", new { id = imagen.numint }, imagen);
        }


        [Route("api/imagen")]
        [HttpGet]
        public ResponseModel Get()
        {
            ResponseModel _objResponseModel = new ResponseModel();
            List<Imagen> imagenes= new List<Imagen>();
            try
            {
                AccesoDatosSQL.AccesoDatosSQL cnx = new AccesoDatosSQL.AccesoDatosSQL(RecursosNavegador.Recursos.GetConfiguration("conexionSQL"));
                DataTable dt = cnx.EjecutarConsultaDT("SELECT [NUMINT],[TIPO],[ImagenDataUrl],[ImagenAsBase64] FROM IMAGEN ");
                foreach (DataRow row in dt.Rows)
                {
                    Imagen ima = new Imagen();
                    ima.numint = row["NUMINT"].ToString();
                    ima.imagenDataUrl = row["ImagenDataUrl"].ToString();
                    ima.imagenAsBase64 = row["ImagenAsBase64"].ToString();
                    ima.tipo = Convert.ToInt32(row["TIPO"]);
                    imagenes.Add(ima);
                }
                _objResponseModel.Data = imagenes;
                _objResponseModel.Status = true;
                _objResponseModel.Message = "Data Received successfully";

            }
            catch (Exception ex)
            {
                _objResponseModel.Data = imagenes;
                _objResponseModel.Status = false;
                _objResponseModel.Message = "Data Failed to receive";
                throw new Exception("Error : " + ex.Message);
            }
            return _objResponseModel;
        }

        //   [HttpGet]
        // [Route("{numint:alpha}")]
        [Route("api/imagen/{numint}")]
        [HttpGet]
        public ResponseModel Get(String numint)
        {
            ResponseModel _objResponseModel = new ResponseModel();
            Imagen ima = new Imagen();
            try
            {
                AccesoDatosSQL.AccesoDatosSQL cnx = new AccesoDatosSQL.AccesoDatosSQL(RecursosNavegador.Recursos.GetConfiguration("conexionSQL"));
                DataTable dt = cnx.EjecutarConsultaDT("SELECT [NUMINT],[TIPO],[ImagenDataUrl],[ImagenAsBase64] FROM IMAGEN WHERE NUMINT ='"+numint+"'");
                foreach (DataRow row in dt.Rows)
                {
                    
                    ima.numint = row["NUMINT"].ToString();
                    ima.imagenDataUrl = row["ImagenDataUrl"].ToString();
                    ima.imagenAsBase64 = row["ImagenAsBase64"].ToString();
                    ima.tipo = Convert.ToInt32(row["TIPO"]);
                    
                }
                _objResponseModel.Data = ima;
                _objResponseModel.Status = true;
                _objResponseModel.Message = "Data Received successfully";

            }
            catch (Exception ex)
            {
                _objResponseModel.Data = ima;
                _objResponseModel.Status = false;
                _objResponseModel.Message = "Data Failed to receive";
                throw new Exception("Error : " + ex.Message);
            }
            return _objResponseModel;
        }

    }
}
