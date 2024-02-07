using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using IBM.Data.DB2.iSeries;

namespace AccesoDatos
{
    public class AccesoDatos
    {
        private iDB2Command _comando = null;
        private iDB2Connection _conexion = null;
        private iDB2Transaction _transaccion = null;
        private string _stringConexion = "";

        public AccesoDatos()
        {
        }
        
        public AccesoDatos(string stringConexion)
        {
            this._stringConexion = stringConexion;
        }

        public void abrirConexion()
        {
           try{
               this._conexion = new iDB2Connection();
               this._conexion.ConnectionString = this._stringConexion;

               if (this._conexion.State == ConnectionState.Closed)
                   this._conexion.Open();
           }
           catch (Exception ex)
           {
               throw ex;
           }
        }

        public void cerrarConexion()
        {
            try
            {
                if (this._conexion.State != ConnectionState.Closed)
                    this._conexion.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void CrearComando(string sentenciaSQL)
        {
            this._comando = new iDB2Command();
            this._comando.Connection = this._conexion;
            this._comando.CommandType = CommandType.Text;

            this._comando.CommandText = sentenciaSQL;
            if (this._transaccion != null)
                this._comando.Transaction = this._transaccion;
        }

        public void CrearComandoSParam(string nombreSP, iDB2Command cmd)
        {
            this._comando = new iDB2Command();
            cmd.Connection = this._conexion;
            cmd.CommandText = nombreSP;
            cmd.CommandType = CommandType.StoredProcedure;
            this._comando = cmd;
            if (this._transaccion != null)
                this._comando.Transaction = this._transaccion;
        }

        public void CrearComandoSP(string nombreSP)
        {
            this._comando = new iDB2Command();
            this._comando.Connection = this._conexion;
            this._comando.CommandText = nombreSP;
            this._comando.CommandType = CommandType.StoredProcedure;
            this._comando.CommandTimeout = 0;
            if (this._transaccion != null)
                this._comando.Transaction = this._transaccion;
        }

        public void CrearComandoSP(string nombreSP, iDB2Command cmd)
        {
            this._comando = new iDB2Command();
            cmd.Connection = this._conexion;
            cmd.CommandText = nombreSP;
            cmd.CommandType = CommandType.StoredProcedure;
            this._comando = cmd;
            if (this._transaccion != null)
                this._comando.Transaction = this._transaccion;
        }
        
        public DataTable EjecutarConsultaDT(string stringSQL)
        {
            abrirConexion();
            DataTable dt = new DataTable();
            iDB2DataAdapter adapter = new iDB2DataAdapter();
            adapter.SelectCommand = new iDB2Command(stringSQL, this._conexion);
            adapter.Fill(dt);
            cerrarConexion();
            return dt;
        }

        public DataSet EjecutarConsultaDataSet(string stringSQL, string nombreTabla)
        {
            abrirConexion();
            DataSet ds = new DataSet();
            iDB2DataAdapter adapter = new iDB2DataAdapter(stringSQL, this._conexion);
            ds.Clear();
            adapter.Fill(ds, nombreTabla);
            cerrarConexion();
            return ds;
        }

        public void EjecutarComando()
        {
            this._comando.ExecuteNonQuery();
        }

        public void ComenzarTransaccion()
        {
            if (this._transaccion == null)
                this._transaccion = this._conexion.BeginTransaction();
        }

        public void CancelarTransaccion()
        {
            if (this._transaccion != null)
            {
                this._transaccion.Rollback();
                cerrarConexion();
            }
        }

        public void TerminarTransaccion()
        {
            if (this._transaccion != null)
                this._transaccion = null;
        }

        public void ConfirmarTransaccion()
        {
            if (this._transaccion != null)
                this._transaccion.Commit();
        }

        public void EjecutaQry(string stringSQL)
        {
            try
            {
                abrirConexion();
                //ComenzarTransaccion();
                CrearComando(stringSQL);
                EjecutarComando();
                //ConfirmarTransaccion();
                cerrarConexion();
            }
            catch (iDB2Exception ex)
            {
                this.TerminarTransaccion();
                cerrarConexion();
                throw new Exception(" Error " + ex.Message);
            }
        }

        public void EjecutaQrypARAMusr(string stringSQL)
        {
            try
            {
                abrirConexion();
                //ComenzarTransaccion();
                CrearComando(stringSQL);
                EjecutarComando();
                //ConfirmarTransaccion();
                cerrarConexion();
            }
            catch (iDB2Exception ex)
            {
                this.TerminarTransaccion();
                cerrarConexion();
                throw new Exception(" Error " + ex.Message);
            }
        }

        public void EjecutaQrySP(string nombreSP)
        {
            try
            {
                abrirConexion();
                //ComenzarTransaccion();
                CrearComandoSP(nombreSP);
                EjecutarComando();
                //ConfirmarTransaccion();
                cerrarConexion();
            }
            catch (iDB2Exception ex)
            {
                this.TerminarTransaccion();
                cerrarConexion();
                throw new Exception(" Error " + ex.Message);
            }
        }

        public void EjecutaQrySParam(string nombreSP, iDB2Command cmd)
        {
            try
            {
                abrirConexion();
                //ComenzarTransaccion();
                CrearComandoSP(nombreSP, cmd);
                EjecutarComando();
                //ConfirmarTransaccion();
                cerrarConexion();
            }
            catch (iDB2Exception ex)
            {
                this.TerminarTransaccion();
                cerrarConexion();
                throw new Exception(" Error " + ex.Message);
            }
        }        
    }
}