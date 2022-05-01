using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Globalization;

namespace GeradorDeScriptDecodificar
{
    public partial class Inicio : Form
    {
        string codCheckout = string.Empty;
        string codPaygo = string.Empty;
        string bandeira = string.Empty;
        string dataHora = string.Empty;
        string dataComprovante = string.Empty;
        string horaComprovante = string.Empty;
        string nsu = string.Empty;
        string identificadorAdquirete = string.Empty;
        string redeAdquirente = string.Empty;
        string codAutorizacao = string.Empty;
        string quantidadeParcelas = string.Empty;
        string codColCxa = string.Empty;
        string idFormaPagto = string.Empty;
        string idFormaPagamento = string.Empty;
        string codCxa = string.Empty;
        string codUsuario = string.Empty;
        string codSessaoCaixa = string.Empty;
        string valor = string.Empty;
        string compCustomizado = string.Empty;
        string idDecRegistroTransacao = string.Empty;
        string valorparcela = string.Empty;
        //debito
        string[] idlanArray;
        string[] idbaixaArray;
        string[] valoresArray;

        double valorFormaPagamento = 0.00;

        //credito

        string[] idlanCredArray;
        string[] idbaixaCred01Array;
        string[] valorprimeiraParcela1Array;
        string[] ValoroutrasParcelas1Array;

        string valorprimeiraParcela1 = string.Empty;
        string valoroutrasParcelas1 = string.Empty;

        string decregistrotransacao = string.Empty;
        string DECREGISTROTRANSACAOPAYGO = string.Empty;
        string decregistrotransacaoformapagamento = string.Empty;
        string DECPARCELAPAGAMENTO = string.Empty;

        string scriptGerado = string.Empty;

        bool cbxidBaixa = false;
        bool cbxparcelaPagamento = false;
        bool cbxformaPagamento = false;
        bool cbxpaygo = false;
        bool cbxregistroTransacao = false;
        bool cbxalteraValorDRT = false;
        bool cbxvendaDebito = false;
        bool cbxvendaCredito = false;

        bool cbxidBaixaCred1 = false;

        //CultureInfo CurrentUICulture = new CultureInfo("pt-BR");

        public Inicio()
        {
            InitializeComponent();
            habiliterCBXDebito();
        }

        private void popularVariaveis()
        {
            codCheckout = txtCodCheckout.Text;
            codPaygo = txtCodPaygo.Text;
            bandeira = txtBandeira.Text;

            dataComprovante = txtDataComprovante.Text;
            horaComprovante = txtHoraComprovante.Text;
            nsu = txtNSU.Text;
            identificadorAdquirete = txtIndentificadorAdquirente.Text;
            redeAdquirente = txtRedeAdquirente.Text;
            codAutorizacao = txtCodAutorizacao.Text;
            quantidadeParcelas = txtQntParcelas.Text;
            codColCxa = txtCodColCxa.Text;
            idFormaPagto = txtIdFormaPagto.Text;
            idFormaPagamento = txtIdFormaPagamento.Text;
            codCxa = txtCodCxa.Text;
            codUsuario = txtCodUsuario.Text;
            codSessaoCaixa = txtCodSessaoCaixa.Text;
            valor = txtValor.Text;
            idDecRegistroTransacao = txtIdDecRegistroTransacao.Text;

            var dataHora2 = string.IsNullOrEmpty(txtDataHora.Text.ToString()) ? "0000-00-00 00:00:00" : txtDataHora.Text;
            dataHora = dataHora2;

            var verificavalorFormaPagamento = string.IsNullOrEmpty(txtValor.Text.ToString()) ? "0.00" : txtValor.Text;

            valorFormaPagamento = double.Parse(verificavalorFormaPagamento, CultureInfo.InvariantCulture);
            //dataCompro = (txtDataHora.Text).ToString("yyyy-MM-dd");  //string.IsNullOrEmpty(txtDataHora.ToString()) ? "" : Convert.ToDateTime(txtDataHora).ToString("yyyy-MM-ddd");
            //txtIdFormaPagto.Enabled = false;

            cbxidBaixa = cbxIdBaixa1.Checked;
            cbxparcelaPagamento = cbxParcelaPagamento.Checked;
            cbxformaPagamento = cbxFormaPagamento.Checked;
            cbxpaygo = cbxPaygo.Checked;
            cbxregistroTransacao = cbxRegistroTransacao.Checked;
            cbxalteraValorDRT = cbxAlteraValorDRT.Checked;
            cbxvendaDebito = cbxVendaDebito.Checked;
            cbxvendaCredito = cbxVendaCredito.Checked;

            cbxidBaixaCred1 = cbxIdBaixaCred1.Checked;


            if (cbxvendaDebito == true)
            {
                var idLan = txtIdLan.Text;
                var idLanArray = idLan.Split(',');
                //idLan = String.Join(",", idLanArray); // para inverte a instrução acima
                idlanArray = idLanArray;

                var idBaixa = txtIdBaixa01.Text;
                var idBaixaArray = idBaixa.Split(',');
                idbaixaArray = idBaixaArray;

                var valores1 = Valores1.Text;
                var ValoresArray = valores1.Split(',');
                valoresArray = ValoresArray;
            }

            if (cbxvendaCredito == true)
            {
                var idLan = txtIdLanCred.Text;
                var idLanCredArray = idLan.Split(',');
                //idLan = String.Join(",", idLanArray); // para inverte a instrução acima
                idlanCredArray = idLanCredArray;

                var idBaixaCred01 = txtIdBaixaCred01.Text;
                var idBaixaCred01Array = idBaixaCred01.Split(',');
                idbaixaCred01Array = idBaixaCred01Array;

                var valorprimeiraParcela1 = valorPrimeiraParcela1.Text;
                var valorPrimeiraParcela1Array = valorprimeiraParcela1.Split(',');
                valorprimeiraParcela1Array = valorPrimeiraParcela1Array;

                var valoroutrasParcelas1 = valorOutrasParcelas1.Text;
                var ValorOutrasParcelas1Array = valoroutrasParcelas1.Split(',');
                ValoroutrasParcelas1Array = ValorOutrasParcelas1Array;

                valorprimeiraParcela1 = valorPrimeiraParcela1.Text;
                valoroutrasParcelas1 = valorOutrasParcelas1.Text;

            }

        }

        private string scriptDecParcelaPagamento(bool ifelse, int il, int ib, int qnt, string valorparcela)
        {
            var script = string.Empty;

            if (ifelse)
            {

                script =

            "INSERT INTO DECPARCELAPAGAMENTO(CODCOLIGADA, IDLAN, VALOR, DATAHORA, CODCHECKOUT, CODIGOPAYGO, CODUSUARIO, CODCOLCXA, CODCXA," +
            " VALORJUROS, VALORDESCONTO, VALORMULTA, TBCRETORNO, TBCSTATUS, IDBAIXA, CODSESSAOCAIXA, STATUS, VALORJUROSALTERACAO," +
            " VALORDESCONTOALTERACAO, VALORMULTAALTERACAO, IDFORMAPAGTO, GERADA_POR_FALHAGERAL, IDCHEQUE, LIQUIDOU_CHEQUE_APOS_DEVOLUCAO," +
            " IDFORMAPAGAMENTO, NSU, CODIGOAUTORIZACAO, PARCELA, CAMPOLIVRE, DATACRIACAO, ID_DECREGISTROTRANSACAOFORMAPAGAMENTO," +
            " IDLAN_PROTHEUS, ID_DECINTEGRACAOSISTEMA, IDBAIXA_PROTHEUS, IDLAN_WEBQV, IDBAIXA_WEBQV, VALORJUROS_PROTHEUS, VALORMULTA_PROTHEUS," +
            " VALORJUROS_WEBQV, VALORMULTA_WEBQV)" +
            " VALUES " +
            $"(@CODCOLIGADA, {idlanCredArray[il]}, {valorparcela}, '{dataHora}', '{codCheckout}', '{codPaygo}', '{codUsuario}', '{codColCxa}', '{codCxa}', 0.00, 0.00, 0.00," +
            $" '1', 1, {idbaixaCred01Array[ib]}, {codSessaoCaixa}, 0, 0, 0, 0, {idFormaPagto}, '', 0, 0, {idFormaPagamento}, '{nsu}', '{codAutorizacao}', {qnt}, " +
            $"'NSU: {nsu} - Aut: {codAutorizacao} - Parcela: {qnt} - Cartão de Crédito', getdate(), @ID_DECREGISTROTRANSACAOFORMAPAGAMENTO, '', 1, 0, ''," +
            " 0, 0.00, 0.00, 0.00, 0.00)" + "\n" + "\n";

            }

            else
            {

                script =
               "INSERT INTO DECPARCELAPAGAMENTO(CODCOLIGADA, IDLAN, VALOR, DATAHORA, CODCHECKOUT, CODIGOPAYGO, CODUSUARIO, CODCOLCXA, CODCXA," +
               " VALORJUROS, VALORDESCONTO, VALORMULTA, TBCRETORNO, TBCSTATUS, IDBAIXA, CODSESSAOCAIXA, STATUS, VALORJUROSALTERACAO," +
               " VALORDESCONTOALTERACAO, VALORMULTAALTERACAO, IDFORMAPAGTO, GERADA_POR_FALHAGERAL, IDCHEQUE, LIQUIDOU_CHEQUE_APOS_DEVOLUCAO," +
               " IDFORMAPAGAMENTO, NSU, CODIGOAUTORIZACAO, PARCELA, CAMPOLIVRE, DATACRIACAO, ID_DECREGISTROTRANSACAOFORMAPAGAMENTO," +
               " IDLAN_PROTHEUS, ID_DECINTEGRACAOSISTEMA, IDBAIXA_PROTHEUS, IDLAN_WEBQV, IDBAIXA_WEBQV, VALORJUROS_PROTHEUS, VALORMULTA_PROTHEUS," +
               " VALORJUROS_WEBQV, VALORMULTA_WEBQV)" +
               " VALUES " +
               $"(@CODCOLIGADA, {idlanCredArray[il]}, {valorparcela}, '{dataHora}', '{codCheckout}', '{codPaygo}', '{codUsuario}', '{codColCxa}', '{codCxa}', 0.00, 0.00, 0.00," +
               $" 'REENVIAR', 0, 0, {codSessaoCaixa}, 0, 0, 0, 0, {idFormaPagto}, '', 0, 0, {idFormaPagamento}, '{nsu}', '{codAutorizacao}', {qnt}, " +
               $"'NSU: {nsu} - Aut: {codAutorizacao} - Parcela: {qnt} - Cartão de Crédito', getdate(), @ID_DECREGISTROTRANSACAOFORMAPAGAMENTO, '', 1, 0, ''," +
               " 0, 0.00, 0.00, 0.00, 0.00)" + "\n" + "\n";

            }
            return script;


        }

        private string GerarScript()
        {
            if (cbxvendaDebito == true)
            {
                #region venda Débito

                //tabela decregistrotransacao

                if (cbxregistroTransacao == true)
                {
                    if (cbxalteraValorDRT == true)
                    {
                        string AlteraValor =

                            "declare @VALOR float" + "\n" +
                            $"SELECT  @VALOR = SUM(x.valor) FROM(SELECT valor FROM decregistrotransacao where codcheckout = '{codCheckout}' UNION ALL SELECT valor = {valor}) x" + "\n" +
                            $"update decregistrotransacao set valor = @VALOR, INATIVADA = 0 where codcheckout = '{codCheckout}'" +
                            "\n" +
                            "\n";

                        decregistrotransacao = AlteraValor;
                    }
                    if (cbxalteraValorDRT == false)
                    {
                        string naoAlteraValor =
                       $"update decregistrotransacao  set INATIVADA = 0 where codcheckout = '{codCheckout}'" +
                       "\n" +
                       "\n";

                        decregistrotransacao = naoAlteraValor;
                    }
                }

                //tabela DECREGISTROTRANSACAOPAYGO

                if (cbxpaygo == true)
                {
                    string scriptPaygo =
                    "INSERT INTO DECREGISTROTRANSACAOPAYGO(DATAHORA, CODCHECKOUT, CODIGOPAYGO, COMPROVANTEESTABELECIMENTO, COMPROVANTECLIENTE," +
                    " IDFORMAPAGTO, REDEADQUIRENTE, NSU, CODIGOAUTORIZACAO, QUANTIDADEPARCELAS, DATACOMPROVANTE, HORACOMPROVANTE, OPERACAO," +
                    " TIPOCARTAO, TIPOFINANCIAMENTO, INDICEREDEADQUIRENTE, TEFCANCELAMENTO, VALOR, IDFORMAPAGAMENTO, BANDEIRA, ESTORNADO," +
                    " DATAESTORNO, CODCHECKOUTREFCANCELAMENTO, COMPROVANTEREDUZIDO, COMPROVANTECUSTOMIZADO)" +
                    " VALUES " +
                    $"('{dataHora}', '{codCheckout}', '{codPaygo}', NULL, NULL, NULL, '{redeAdquirente}', '{nsu}', '{codAutorizacao}', {quantidadeParcelas}," +
                    $" '{dataComprovante}', '{horaComprovante}', 1, 2, 1, '{identificadorAdquirete}', 0, {valor}, {idFormaPagamento},'{bandeira}', 0," +
                    $" '1900-01-01', '0', '0', '0')" +
                    "\n" +
                    "\n";

                    DECREGISTROTRANSACAOPAYGO = scriptPaygo;
                }

                //tabela decregistrotransacaoformapagamento

                if (cbxformaPagamento == true)
                {
                    string scriptFormaPagamento =
                   "INSERT INTO decregistrotransacaoformapagamento(ID_DECREGISTROTRANSACAO, IDFORMAPAGAMENTO, VALOR, FALHOU, CODCOLCXA, CODCXA," +
                    " TROCO, POS_NSU, POS_CODIGOAUTORIZACAO, POS_QUANTIDADEPARCELAS, POS_NOMECARTAO, POS_INSTITUICAOFINANCEIRA, POS_DATAEXPIRACAO," +
                    " POS_6DIG_BIN, POS_ULTIMO4DIGITO, POS_BANDEIRA, CODCHECKOUT, CODIGOPAYGO, IDFORMAPAGAMENTO_DEPARA, DATACOMPROVANTE, ESTORNADO," +
                    " DATAESTORNO, COMPROVANTECUSTOMIZADO, POS_IDMAQUINAPOS)" +
                    " VALUES " +
                    $"({idDecRegistroTransacao}, {idFormaPagamento}, {valor}, 0, '{codColCxa}', '{codCxa}', 0.00, '{nsu}', '{codAutorizacao}', " +
                    $"{quantidadeParcelas}, '', '{redeAdquirente}', '', '', '', '{bandeira}', '{codCheckout}', '{codPaygo}', {idFormaPagamento}, '{dataHora.Remove(dataHora.Length - 9)}', 0, NULL, " +
                    $"'R$ {valorFormaPagamento.ToString("F2")} VENDA DÉBITO {bandeira} ************ DOC: {nsu} AUTORIZ: {codAutorizacao}', 0)" +
                    "\n" +
                    "\n";

                    decregistrotransacaoformapagamento = scriptFormaPagamento;
                }

                //string DECPARCELAPAGAMENTO = string.Empty;


                string ExisteBaixa = string.Empty;
                for (int i = 0; i < idlanArray.Length; i++)
                {

                    if (cbxparcelaPagamento == true)
                    {

                        if (cbxidBaixa == true)
                        {

                            ExisteBaixa +=

                        "INSERT INTO DECPARCELAPAGAMENTO(CODCOLIGADA, IDLAN, VALOR, DATAHORA, CODCHECKOUT, CODIGOPAYGO, CODUSUARIO, CODCOLCXA, CODCXA," +
                        " VALORJUROS, VALORDESCONTO, VALORMULTA, TBCRETORNO, TBCSTATUS, IDBAIXA, CODSESSAOCAIXA, STATUS, VALORJUROSALTERACAO," +
                        " VALORDESCONTOALTERACAO, VALORMULTAALTERACAO, IDFORMAPAGTO, GERADA_POR_FALHAGERAL, IDCHEQUE, LIQUIDOU_CHEQUE_APOS_DEVOLUCAO," +
                        " IDFORMAPAGAMENTO, NSU, CODIGOAUTORIZACAO, PARCELA, CAMPOLIVRE, DATACRIACAO, ID_DECREGISTROTRANSACAOFORMAPAGAMENTO," +
                        " IDLAN_PROTHEUS, ID_DECINTEGRACAOSISTEMA, IDBAIXA_PROTHEUS, IDLAN_WEBQV, IDBAIXA_WEBQV, VALORJUROS_PROTHEUS, VALORMULTA_PROTHEUS," +
                        " VALORJUROS_WEBQV, VALORMULTA_WEBQV)" +
                        " VALUES " +
                        $"(@CODCOLIGADA, {idlanArray[i]}, {valoresArray[i]}, '{dataHora}', '{codCheckout}', '{codPaygo}', '{codUsuario}', '{codColCxa}', '{codCxa}', 0.00, 0.00, 0.00," +
                        $" '1', 1, {idbaixaArray[i]}, {codSessaoCaixa}, 0, 0, 0, 0, {idFormaPagto}, '', 0, 0, {idFormaPagamento}, '{nsu}', '{codAutorizacao}', {quantidadeParcelas}, " +
                        $"'NSU: {nsu} - Aut: {codAutorizacao} - Parcela: {quantidadeParcelas} - Cartão de Débito', getdate(), @ID_DECREGISTROTRANSACAOFORMAPAGAMENTO, '', 1, 0, ''," +
                        " 0, 0.00, 0.00, 0.00, 0.00)" + "\n" + "\n";

                        }

                        if (cbxidBaixa == false)
                        {

                            ExisteBaixa +=
                           "INSERT INTO DECPARCELAPAGAMENTO(CODCOLIGADA, IDLAN, VALOR, DATAHORA, CODCHECKOUT, CODIGOPAYGO, CODUSUARIO, CODCOLCXA, CODCXA," +
                           " VALORJUROS, VALORDESCONTO, VALORMULTA, TBCRETORNO, TBCSTATUS, IDBAIXA, CODSESSAOCAIXA, STATUS, VALORJUROSALTERACAO," +
                           " VALORDESCONTOALTERACAO, VALORMULTAALTERACAO, IDFORMAPAGTO, GERADA_POR_FALHAGERAL, IDCHEQUE, LIQUIDOU_CHEQUE_APOS_DEVOLUCAO," +
                           " IDFORMAPAGAMENTO, NSU, CODIGOAUTORIZACAO, PARCELA, CAMPOLIVRE, DATACRIACAO, ID_DECREGISTROTRANSACAOFORMAPAGAMENTO," +
                           " IDLAN_PROTHEUS, ID_DECINTEGRACAOSISTEMA, IDBAIXA_PROTHEUS, IDLAN_WEBQV, IDBAIXA_WEBQV, VALORJUROS_PROTHEUS, VALORMULTA_PROTHEUS," +
                           " VALORJUROS_WEBQV, VALORMULTA_WEBQV)" +
                           " VALUES " +
                           $"(@CODCOLIGADA, {idlanArray[i]}, {valoresArray[i]}, '{dataHora}', '{codCheckout}', '{codPaygo}', '{codUsuario}', '{codColCxa}', '{codCxa}', 0.00, 0.00, 0.00," +
                           $" 'REENVIAR', 0, 0, {codSessaoCaixa}, 0, 0, 0, 0, {idFormaPagto}, '', 0, 0, {idFormaPagamento}, '{nsu}', '{codAutorizacao}', {quantidadeParcelas}, " +
                           $"'NSU: {nsu} - Aut: {codAutorizacao} - Parcela: {quantidadeParcelas} - Cartão de Débito', getdate(), @ID_DECREGISTROTRANSACAOFORMAPAGAMENTO, '', 1, 0, ''," +
                           " 0, 0.00, 0.00, 0.00, 0.00)" + "\n" + "\n";


                        }

                    }

                }

                DECPARCELAPAGAMENTO = ExisteBaixa;

                #endregion
            }
            if (cbxvendaCredito == true)
            {
                #region venda Crédito

                //tabela decregistrotransacao

                if (cbxregistroTransacao == true)
                {
                    if (cbxalteraValorDRT == true)
                    {
                        string AlteraValor =
                            "declare @VALOR float" + "\n" +
                            $"SELECT  @VALOR = SUM(x.valor) FROM(SELECT valor FROM decregistrotransacao where codcheckout = '{codCheckout}' UNION ALL SELECT valor = {valor}) x" + "\n" +
                            $"update decregistrotransacao set valor = @VALOR, INATIVADA = 0 where codcheckout = '{codCheckout}'" +
                            "\n" +
                            "\n";

                        decregistrotransacao = AlteraValor;
                    }
                    if (cbxalteraValorDRT == false)
                    {
                        string naoAlteraValor =
                       $"update decregistrotransacao  set INATIVADA = 0 where codcheckout = '{codCheckout}'" +
                       "\n" +
                       "\n";

                        decregistrotransacao = naoAlteraValor;
                    }
                }

                //tabela DECREGISTROTRANSACAOPAYGO

                if (cbxpaygo == true)
                {
                    string scriptPaygo =
                    "INSERT INTO DECREGISTROTRANSACAOPAYGO(DATAHORA, CODCHECKOUT, CODIGOPAYGO, COMPROVANTEESTABELECIMENTO, COMPROVANTECLIENTE," +
                    " IDFORMAPAGTO, REDEADQUIRENTE, NSU, CODIGOAUTORIZACAO, QUANTIDADEPARCELAS, DATACOMPROVANTE, HORACOMPROVANTE, OPERACAO," +
                    " TIPOCARTAO, TIPOFINANCIAMENTO, INDICEREDEADQUIRENTE, TEFCANCELAMENTO, VALOR, IDFORMAPAGAMENTO, BANDEIRA, ESTORNADO," +
                    " DATAESTORNO, CODCHECKOUTREFCANCELAMENTO, COMPROVANTEREDUZIDO, COMPROVANTECUSTOMIZADO)" +
                    " VALUES " +
                    $"('{dataHora}', '{codCheckout}', '{codPaygo}', NULL, NULL, NULL, '{redeAdquirente}', '{nsu}', '{codAutorizacao}', {quantidadeParcelas}," +
                    $" '{dataComprovante}', '{horaComprovante}', 1, 1, 1, '{identificadorAdquirete}', 0, {valor}, {idFormaPagamento},'{bandeira}', 0," +
                    $" '1900-01-01', '0', '0', '0')" +
                    "\n" +
                    "\n";

                    DECREGISTROTRANSACAOPAYGO = scriptPaygo;
                }

                //tabela decregistrotransacaoformapagamento

                if (cbxformaPagamento == true)
                {
                    string scriptFormaPagamento =
                   "INSERT INTO decregistrotransacaoformapagamento(ID_DECREGISTROTRANSACAO, IDFORMAPAGAMENTO, VALOR, FALHOU, CODCOLCXA, CODCXA," +
                    " TROCO, POS_NSU, POS_CODIGOAUTORIZACAO, POS_QUANTIDADEPARCELAS, POS_NOMECARTAO, POS_INSTITUICAOFINANCEIRA, POS_DATAEXPIRACAO," +
                    " POS_6DIG_BIN, POS_ULTIMO4DIGITO, POS_BANDEIRA, CODCHECKOUT, CODIGOPAYGO, IDFORMAPAGAMENTO_DEPARA, DATACOMPROVANTE, ESTORNADO," +
                    " DATAESTORNO, COMPROVANTECUSTOMIZADO, POS_IDMAQUINAPOS)" +
                    " VALUES " +
                    $"({idDecRegistroTransacao}, {idFormaPagamento}, {valor}, 0, '{codColCxa}', '{codCxa}', 0.00, '{nsu}', '{codAutorizacao}', " +
                    $"{quantidadeParcelas}, '', '{redeAdquirente}', '', '', '', '{bandeira}', '{codCheckout}', '{codPaygo}', {idFormaPagamento}, '{dataHora.Remove(dataHora.Length - 9)}', 0, NULL, " +
                    $"'R$ {valorFormaPagamento.ToString("F2")} VENDA CRÉDITO {bandeira} ************ DOC: {nsu} AUTORIZ: {codAutorizacao}', 0)" +
                    "\n" +
                    "\n";

                    decregistrotransacaoformapagamento = scriptFormaPagamento;
                }

                // string DECPARCELAPAGAMENTO = string.Empty;

                string ExisteBaixa = string.Empty;

                int ibx = 0;
                for (int il = 0; il < idlanCredArray.Length; il++)
                {

                    int qnt = 0;

                    if (cbxIdBaixaCred1.Checked == true)
                    {
                        for (int ib = 0; ib < idbaixaCred01Array.Length; ib++)
                        {

                            if (ibx + 1 >= int.Parse(quantidadeParcelas) && int.Parse(quantidadeParcelas) > 1)
                            {
                                
                                ib = ibx;
                                ib++;
                                ibx = 0;

                            }
                            if (cbxparcelaPagamento == true)
                            {

                                if (qnt < int.Parse(quantidadeParcelas))
                                {
                                    qnt += +1;
                                }
                                if (qnt > int.Parse(quantidadeParcelas))
                                {
                                    qnt = 1;
                                }
                                if (qnt == 1)
                                {
                                    valorparcela = valorprimeiraParcela1Array[il];
                                }
                                if (qnt != 1)
                                {
                                    valorparcela = ValoroutrasParcelas1Array[il];
                                }


                                ExisteBaixa += scriptDecParcelaPagamento(cbxidBaixaCred1, il, ib, qnt, valorparcela);

                            }
                            if (qnt == int.Parse(quantidadeParcelas))
                            {
                                ibx = ib;
                                break;
                            }
                        }
                    }
                    else
                    {
                        for (int ib = 0; ib < int.Parse(quantidadeParcelas); ib++)
                        {

                            if (ibx + 1 >= int.Parse(quantidadeParcelas) && int.Parse(quantidadeParcelas) > 1)
                            {
                                ibx = 0;
                                ib = ibx;
                                //ib++;                                
                            }
                            if (cbxparcelaPagamento == true)
                            {

                                if (qnt < int.Parse(quantidadeParcelas))
                                {
                                    qnt += +1;
                                }
                                if (qnt > int.Parse(quantidadeParcelas))
                                {
                                    qnt = 1;
                                }
                                if (qnt == 1)
                                {
                                    valorparcela = valorprimeiraParcela1Array[il];
                                }
                                if (qnt != 1)
                                {
                                    valorparcela = ValoroutrasParcelas1Array[il];
                                }


                                ExisteBaixa += scriptDecParcelaPagamento(cbxidBaixaCred1, il, ib, qnt, valorparcela);

                            }
                            if (qnt == int.Parse(quantidadeParcelas))
                            {
                                ibx = ib;
                                break;
                            }
                        }
                    }

                }

                DECPARCELAPAGAMENTO = ExisteBaixa;

                #endregion
            }

            if (cbxparcelaPagamento == true)
            {
                scriptGerado = decregistrotransacao +
                            DECREGISTROTRANSACAOPAYGO +
                            decregistrotransacaoformapagamento +
                            "declare @ID_DECREGISTROTRANSACAOFORMAPAGAMENTO int, " +
                            "\n" +
                                    "@CODCOLIGADA int, " +
                            "\n" +
                            $"SELECT  @ID_DECREGISTROTRANSACAOFORMAPAGAMENTO = ID FROM DECREGISTROTRANSACAOFORMAPAGAMENTO (NOLOCK) WHERE CODCHECKOUT = '{codCheckout}' AND POS_NSU = '{nsu}'" +
                            "\n" +
                            $"SELECT  @CODCOLIGADA = CODCOLIGADA FROM DECREGISTROTRANSACAO (NOLOCK) WHERE ID = {idDecRegistroTransacao}" +
                            "\n" +
                            "\n" +
                            DECPARCELAPAGAMENTO;
            }
            if (cbxparcelaPagamento == false)
            {
                scriptGerado = decregistrotransacao +
                           DECREGISTROTRANSACAOPAYGO +
                           decregistrotransacaoformapagamento;
            }

            return scriptGerado;
        }

        private void btGerarScript_Click(object sender, EventArgs e)
        {
            decregistrotransacao = string.Empty;
            DECREGISTROTRANSACAOPAYGO = string.Empty;
            decregistrotransacaoformapagamento = string.Empty;
            DECPARCELAPAGAMENTO = string.Empty;

            scriptGerado = string.Empty;

            popularVariaveis();
            GerarScript();
            saveFileDialog1.Filter = "Sql files (*.sql)|*.sql|Text files (*.txt)|*.txt|All files (*.*)|*.*";
            saveFileDialog1.ShowDialog();//para salvar           
        }

        private void SalvarOk(object sender, CancelEventArgs e)
        {
            string Caminho = saveFileDialog1.FileName;
            File.WriteAllText(Caminho, scriptGerado);
            scriptGerado = string.Empty;
           // txtIdFormaPagto.Enabled = true;
        }

        private void cbxVendaCredito_Click(object sender, EventArgs e)
        {
            habiliterCBXCredito();
        }

        private void cbxVendaDebito_Click(object sender, EventArgs e)
        {
            habiliterCBXDebito();
        }

        private void habiliterCBXDebito()
        {
            cbxVendaCredito.Checked = false;
            cbxVendaDebito.Checked = true;

            txtIdLanCred.Enabled = false;
            valorPrimeiraParcela1.Enabled = false;
            valorOutrasParcelas1.Enabled = false;
            txtIdBaixaCred01.Enabled = false;
            cbxIdBaixaCred1.Enabled = false;

            txtIdLan.Enabled = false;
            Valores1.Enabled = false;
            txtIdBaixa01.Enabled = false;
            cbxIdBaixa1.Enabled = false;

            if (cbxParcelaPagamento.Checked == true)
            {

                txtIdLanCred.Enabled = false;
                valorPrimeiraParcela1.Enabled = false;
                valorOutrasParcelas1.Enabled = false;
                txtIdBaixaCred01.Enabled = false;
                cbxIdBaixaCred1.Enabled = false;

                txtIdLan.Enabled = true;
                Valores1.Enabled = true;
                txtIdBaixa01.Enabled = true;
                cbxIdBaixa1.Enabled = true;
            }
        }

        private void habiliterCBXCredito()
        {
            cbxVendaDebito.Checked = false;
            cbxVendaCredito.Checked = true;

            txtIdLan.Enabled = false;
            Valores1.Enabled = false;
            txtIdBaixa01.Enabled = false;
            cbxIdBaixa1.Enabled = false;

            txtIdLanCred.Enabled = false;
            valorPrimeiraParcela1.Enabled = false;
            valorOutrasParcelas1.Enabled = false;
            txtIdBaixaCred01.Enabled = false;
            cbxIdBaixaCred1.Enabled = false;

            if (cbxParcelaPagamento.Checked == true)
            {

                txtIdLan.Enabled = false;
                Valores1.Enabled = false;
                txtIdBaixa01.Enabled = false;
                cbxIdBaixa1.Enabled = false;

                txtIdLanCred.Enabled = true;
                valorPrimeiraParcela1.Enabled = true;
                valorOutrasParcelas1.Enabled = true;
                txtIdBaixaCred01.Enabled = true;
                cbxIdBaixaCred1.Enabled = true;
            }
        }

        private void cbxParcelaPagamento_Click(object sender, EventArgs e)
        {
            if (cbxParcelaPagamento.Checked == true)
            {
                if (cbxVendaDebito.Checked == true)
                {
                    txtIdLanCred.Enabled = false;
                    valorPrimeiraParcela1.Enabled = false;
                    valorOutrasParcelas1.Enabled = false;
                    txtIdBaixaCred01.Enabled = false;
                    cbxIdBaixaCred1.Enabled = false;

                    txtIdLan.Enabled = true;
                    Valores1.Enabled = true;
                    txtIdBaixa01.Enabled = true;
                    cbxIdBaixa1.Enabled = true;
                }
                else
                {
                    txtIdLan.Enabled = false;
                    Valores1.Enabled = false;
                    txtIdBaixa01.Enabled = false;
                    cbxIdBaixa1.Enabled = false;

                    txtIdLanCred.Enabled = true;
                    valorPrimeiraParcela1.Enabled = true;
                    valorOutrasParcelas1.Enabled = true;
                    txtIdBaixaCred01.Enabled = true;
                    cbxIdBaixaCred1.Enabled = true;
                }
            }
            else
            {
                txtIdLan.Enabled = false;
                Valores1.Enabled = false;
                txtIdBaixa01.Enabled = false;
                cbxIdBaixa1.Enabled = false;

                txtIdLanCred.Enabled = false;
                valorPrimeiraParcela1.Enabled = false;
                valorOutrasParcelas1.Enabled = false;
                txtIdBaixaCred01.Enabled = false;
                cbxIdBaixaCred1.Enabled = false;
            }

        }

        private void LimparCampos_Click(object sender, EventArgs e)
        {
            UI ui = new UI(); //instancia a classe UI
            ui.LimpaCampos(this.groupBox1.Controls); //Passa para o método todos os controles que estão dentro do panel
            ui.LimpaCampos(this.groupBox2.Controls);
            ui.LimpaCampos(this.groupBox3.Controls);
            ui.LimpaCampos(this.groupBox4.Controls);

            cbxIdBaixa1.Checked = false;
            cbxParcelaPagamento.Checked = false;
            cbxFormaPagamento.Checked = false;
            cbxPaygo.Checked = false;
            cbxRegistroTransacao.Checked = false;
            cbxAlteraValorDRT.Checked = false;            

            cbxIdBaixaCred1.Checked = false;

            habiliterCBXDebito();
        }
    }
}
