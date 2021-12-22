using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;
using System.Threading;
using System.Windows.Forms.DataVisualization.Charting;

namespace Jamae
{
    public partial class Form1 : Form
    {
        private const int SELL_PRICE = 40000;

        static int prevVolume = 0;
        private BackgroundWorker bw = new BackgroundWorker(); //BackgroundWorker클래스를 선언 및 할당
        List<PriceInfoEntityObject> priceInfoList;

        static private double prevLastTime;

        static int tmp_count = 0;

        static bool isBuy = false;

        static double buy_price = 0;

        static string trx_price = "";
        static string buy_price_to_alt = "";

        public Form1()
        {
            InitializeComponent();

            chart1.Series["sCandle"]["PriceUpColor"] = "Red";
            chart1.Series["sCandle"]["PriceDownColor"] = "Blue";
            chart1.ChartAreas[0].AxisY.LabelStyle.Format = "{0:#,0}";
            chart1.ChartAreas[0].AxisX.LabelStyle.Format = "HH:mm";
            chart1.ChartAreas[1].AxisX.LabelStyle.Format = "HH:mm";
            chart1.ChartAreas[2].AxisX.LabelStyle.Format = "HH:mm";
            chart1.ChartAreas[3].AxisX.LabelStyle.Format = "HH:mm";
            chart1.ChartAreas[4].AxisX.LabelStyle.Format = "HH:mm";
            chart1.ChartAreas[5].AxisX.LabelStyle.Format = "HH:mm";

            chart1.AxisViewChanged += chart1_AxisViewChanged;

            //ReportProgress메소드를 호출하기 위해서 반드시 true로 설정, false일 경우 ReportProgress메소드를 호출하면 exception 발생
            bw.WorkerReportsProgress = true;
            //스레드에서 취소 지원 여부
            bw.WorkerSupportsCancellation = true;
            //스레드가 run시에 호출되는 핸들러 등록
            bw.DoWork += new DoWorkEventHandler(bw_DoWork);
            // ReportProgress메소드 호출시 호출되는 핸들러 등록
            //bw.ProgressChanged += new ProgressChangedEventHandler(bw_ProgressChanged);
            // 스레드 완료(종료)시 호출되는 핸들러 동록
            //bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_RunWorkerCompleted);


        }

        private void Form1_Load(object sender, EventArgs e)
        {
            bw.RunWorkerAsync();
        }

        private void bw_DoWork(object sender, EventArgs e)
        {
            Up U = new Up("W5zjjBBit8GjGd2mY5rOKFuVY12HV17q513qlmdr", "igQC32jg0NBMMUWadSs39iIAWft7sBGiaL801Unx");
            Boolean isInitChartDraw = false;

            #region 자산
            // 자산 조회
            //Console.WriteLine(U.GetAccount());
            //string json = U.GetAccount();
            //json = "[{DOC_NM : 'A'},{DOC_NM : 'B'}]";

            //JObject jObject = JObject.Parse(json);
            JArray account = JArray.Parse(U.GetAccount());

            //Console.WriteLine(jObject.ToString());

            for (int i = 0; i < account.Count; i++)
            {
                var currency = account[i]["currency"].ToString();
                if (currency.Equals("TRX"))
                {
                    var avg_buy_price = account[i]["avg_buy_price"].ToString();
                    Console.WriteLine(currency.ToString() + " : " + avg_buy_price.ToString());
                }
            }
            #endregion

            #region 주문
            // 주문 가능 정보
            //Console.WriteLine(U.GetOrderChance("KRW-BTC"));

            // 개별 주문 조회
            //Console.WriteLine(U.GetOrder("주문 uuid"));

            // 주문 리스트 조회
            //Console.WriteLine(U.GetAllOrder());

            // 주문하기
            //Console.WriteLine(U.MakeOrder("KRW-BTC", UpbitAPI.UpbitOrderSide.bid, 0.001m, 5000000));

            // 주문 취소
            //Console.WriteLine(U.CancelOrder("주문 uuid"));
            #endregion

            #region 시세 정보
            // 마켓 코드 조회
            //Console.WriteLine(U.GetMarkets());


            // 캔들(분, 일, 주, 월) 조회
            //Console.WriteLine(U.GetCandles_Minute("KRW-TRX", UpbitAPI.UpbitMinuteCandleType._1, to: DateTime.Now.AddMinutes(-2), count: 2));
            //Console.WriteLine(U.GetCandles_Day("KRW-BTC", to: DateTime.Now.AddDays(-2), count: 2));
            //Console.WriteLine(U.GetCandles_Week("KRW-BTC", to: DateTime.Now.AddDays(-14), count: 2));
            //Console.WriteLine(U.GetCandles_Month("KRW-BTC", to: DateTime.Now.AddMonths(-2), count: 2));

            // 당일 체결 내역 조회
            //Console.WriteLine(U.GetTicks("KRW-BTC", count: 2));

            JArray currentPriceJArrayTrx;
            JArray currentPriceJArrayBtc;
            string currentPriceStringTrx_price;
            string currentPriceStringTrx_volume;
            string currentPriceStringTrx_ask_bid;
            string currentPriceStringBtc_price;
            string currentPriceStringBtc_volume;
            string currentPriceStringBtc_ask_bid;

            JArray currentOrderBookJArrayTrx;
            JArray currentOrderBookJArrayBtc;

            string currentOrderBookStringTrx_ask_price;
            string currentOrderBookStringTrx_bid_price;
            string currentOrderBookStringTrx_ask_size;
            string currentOrderBookStringTrx_bid_size;

            string currentOrderBookStringBtc_ask_price;
            string currentOrderBookStringBtc_bid_price;
            string currentOrderBookStringBtc_ask_size;
            string currentOrderBookStringBtc_bid_size;

            int currntVolume = 0;

            DataTable table = new DataTable();

            table.Columns.Add("price", typeof(string));
            table.Columns.Add("volume", typeof(string));
            table.Columns.Add("high_price", typeof(string));
            table.Columns.Add("high_volume", typeof(string));
            table.Columns.Add("low_price", typeof(string));
            table.Columns.Add("low_volume", typeof(string));
            table.Columns.Add("bPrice", typeof(string));
            table.Columns.Add("bVolume", typeof(string));

            while (true)
            {
                string priceTrx = U.GetTicks("KRW-TRX", count: 1);
                string priceBtc = U.GetTicks("KRW-BTC", count: 1);

                string oerderBookTrx = U.GetOrderbook("KRW-TRX");
                string oerderBookBtc = U.GetOrderbook("KRW-BTC");



                currentPriceJArrayTrx = JArray.Parse(priceTrx);
                currentPriceJArrayBtc = JArray.Parse(priceBtc);

                currentOrderBookJArrayTrx = JArray.Parse(oerderBookTrx);
                currentOrderBookJArrayBtc = JArray.Parse(oerderBookBtc);

                //Console.WriteLine(currentOrderBookJArrayTrx.ToString());

                currentPriceStringTrx_price = currentPriceJArrayTrx[0]["trade_price"].ToString();
                currentPriceStringTrx_volume = currentPriceJArrayTrx[0]["trade_volume"].ToString();
                currentPriceStringTrx_ask_bid = currentPriceJArrayTrx[0]["ask_bid"].ToString();

                currentPriceStringBtc_price = currentPriceJArrayBtc[0]["trade_price"].ToString();
                currentPriceStringBtc_volume = currentPriceJArrayBtc[0]["trade_volume"].ToString();
                currentPriceStringBtc_ask_bid = currentPriceJArrayBtc[0]["ask_bid"].ToString();

                currentOrderBookStringTrx_ask_price = currentOrderBookJArrayTrx[0]["orderbook_units"][0]["ask_price"].ToString();
                currentOrderBookStringTrx_bid_price = currentOrderBookJArrayTrx[0]["orderbook_units"][0]["bid_price"].ToString();
                currentOrderBookStringTrx_ask_size = currentOrderBookJArrayTrx[0]["orderbook_units"][0]["ask_size"].ToString();
                currentOrderBookStringTrx_bid_size = currentOrderBookJArrayTrx[0]["orderbook_units"][0]["bid_size"].ToString();

                currentOrderBookStringBtc_ask_price = currentOrderBookJArrayBtc[0]["orderbook_units"][0]["ask_price"].ToString();
                currentOrderBookStringBtc_bid_price = currentOrderBookJArrayBtc[0]["orderbook_units"][0]["bid_price"].ToString();
                currentOrderBookStringBtc_ask_size = currentOrderBookJArrayBtc[0]["orderbook_units"][0]["ask_size"].ToString();
                currentOrderBookStringBtc_bid_size = currentOrderBookJArrayBtc[0]["orderbook_units"][0]["bid_size"].ToString();


                currntVolume = convertStringToInto(currentPriceStringTrx_volume);

                trx_price = currentOrderBookStringTrx_bid_price;

                if (currntVolume != prevVolume)
                {
                    prevVolume = currntVolume;

                    //table.Rows.Add(String.Format("{0:#,0}", convertStringToInto(currentPriceStringTrx_price)), String.Format("{0:#,0}", convertStringToInto(currentPriceStringTrx_volume)), String.Format("{0:#,0}", convertStringToInto(currentOrderBookStringTrx_ask_price)), String.Format("{0:#,0}", convertStringToInto(currentOrderBookStringTrx_ask_size)), String.Format("{0:#,0}", convertStringToInto(currentOrderBookStringTrx_bid_price)), String.Format("{0:#,0}", convertStringToInto(currentOrderBookStringTrx_bid_size)), String.Format("{0:#,0}", convertStringToInto(currentPriceStringBtc_price)), currentPriceStringBtc_volume);
                    table.Rows.Add(currentPriceStringTrx_price, currentPriceStringTrx_volume, currentOrderBookStringTrx_ask_price, String.Format("{0:#,0}", convertStringToInto(currentOrderBookStringTrx_ask_size)), convertStringToInto(currentOrderBookStringTrx_bid_price), String.Format("{0:#,0}", convertStringToInto(currentOrderBookStringTrx_bid_size)), String.Format("{0:#,0}", convertStringToInto(currentPriceStringBtc_price)), currentPriceStringBtc_volume);
                    //tbLog.Text = tbMsg;
                    if (this.InvokeRequired)
                    {
                        this.BeginInvoke(new Action(() => rtbBPrice.Clear()));
                        //this.BeginInvoke(new Action(() => dgbTradeInfo.Rows.Add(String.Format("{0:#,0}", convertStringToInto(currentPriceStringTrx_price)), String.Format("{0:#,0}", convertStringToInto(currentPriceStringTrx_volume)), String.Format("{0:#,0}", convertStringToInto(currentOrderBookStringTrx_ask_price)), String.Format("{0:#,0}", convertStringToInto(currentOrderBookStringTrx_ask_size)), String.Format("{0:#,0}", convertStringToInto(currentOrderBookStringTrx_bid_price)), String.Format("{0:#,0}", convertStringToInto(currentOrderBookStringTrx_bid_size)), String.Format("{0:#,0}", convertStringToInto(currentPriceStringBtc_price)), currentPriceStringBtc_volume)));
                        this.BeginInvoke(new Action(() => dgbTradeInfo.Rows.Add(currentPriceStringTrx_price, currentPriceStringTrx_volume,  currentOrderBookStringTrx_ask_price, String.Format("{0:#,0}", convertStringToInto(currentOrderBookStringTrx_ask_size)), currentOrderBookStringTrx_bid_price, String.Format("{0:#,0}", convertStringToInto(currentOrderBookStringTrx_bid_size)), String.Format("{0:#,0}", convertStringToInto(currentPriceStringBtc_price)), currentPriceStringBtc_volume)));
                        if (currentPriceStringTrx_ask_bid.Equals("BID"))
                        {
                            this.BeginInvoke(new Action(() => dgbTradeInfo.Rows[dgbTradeInfo.Rows.Count - 2].Cells[0].Style.BackColor = Color.LightPink));
                        }
                        else
                        {
                            BeginInvoke(new Action(() => dgbTradeInfo.Rows[dgbTradeInfo.Rows.Count - 2].Cells[0].Style.BackColor = Color.LightBlue));
                        }

                        if (currentPriceStringBtc_ask_bid.Equals("BID"))
                        {
                            this.BeginInvoke(new Action(() => dgbTradeInfo.Rows[dgbTradeInfo.Rows.Count - 2].Cells[6].Style.BackColor = Color.LightPink));
                            this.BeginInvoke(new Action(() => rtbBPrice.SelectionColor = Color.Red));

                        }
                        else
                        {
                            this.BeginInvoke(new Action(() => dgbTradeInfo.Rows[dgbTradeInfo.Rows.Count - 2].Cells[6].Style.BackColor = Color.LightBlue));
                            this.BeginInvoke(new Action(() => rtbBPrice.SelectionColor = Color.Blue));
                        }
                        this.BeginInvoke(new Action(() => dgbTradeInfo.FirstDisplayedScrollingRowIndex = dgbTradeInfo.Rows.Count - 1));
                        this.BeginInvoke(new Action(() => rtbBPrice.AppendText(String.Format("{0:#,0}", convertStringToInto(currentOrderBookStringBtc_bid_price)))));


                    }
                    else
                    {
                        dgbTradeInfo.DataSource = table;
                        if (currentPriceStringTrx_ask_bid.Equals("BID"))
                        {
                            dgbTradeInfo.Rows[dgbTradeInfo.Rows.Count - 1].Cells[0].Style.BackColor = Color.Red;
                        }
                        else
                        {
                            dgbTradeInfo.Rows[dgbTradeInfo.Rows.Count - 1].Cells[0].Style.BackColor = Color.Blue;
                        }
                        dgbTradeInfo.FirstDisplayedScrollingRowIndex = dgbTradeInfo.Rows.Count - 1;

                    }
                }

                // 1m Chart
                if (isInitChartDraw == false)
                {
                    inittDrawChar();
                    isInitChartDraw = true;

                    // Initialize values.
                    Calculations();
                }
                else
                {
                    updateDrawChart();
                    //inittDrawChar();

                    // Initialize values.
                    Calculations();

                }

                Thread.Sleep(250);
            }

            // 현재가 정보 조회
            //Console.WriteLine(U.GetTicker("KRW-BTC,KRW-ETH"));

            // 시세 호가 정보(Orderbook) 조회
            //Console.WriteLine(U.GetOrderbook("KRW-BTC,KRW-ETH"));
            #endregion
            Console.ReadLine();
        }

        #region 차트
        private void inittDrawChar()
        {
            Up U = new Up("W5zjjBBit8GjGd2mY5rOKFuVY12HV17q513qlmdr", "igQC32jg0NBMMUWadSs39iIAWft7sBGiaL801Unx");

            priceInfoList = new List<PriceInfoEntityObject>();

            JArray currentChart1MJArrayTrx;
            JArray currentChart1MJArrayBtc;

            string json_candle_1m_trx = U.GetCandles_Minute("KRW-TRX", Up.UpbitMinuteCandleType._1, to: DateTime.Now.AddMinutes(0), count: 50);
            string json_candle_1m_btc = U.GetCandles_Minute("KRW-BTC", Up.UpbitMinuteCandleType._1, to: DateTime.Now.AddMinutes(0), count: 50);

            currentChart1MJArrayTrx = JArray.Parse(json_candle_1m_trx);
            currentChart1MJArrayBtc = JArray.Parse(json_candle_1m_btc);

            // 1분 마다 차트 업데이트 하기 위해서 마지막 시간 저장 hh:mm
            prevLastTime = DateTime.Parse(currentChart1MJArrayBtc[0]["candle_date_time_kst"].ToString()).ToOADate();
            //Console.WriteLine("Count: " + currentChart1MJArrayTrx.Count());
            for (int nldx = 0; nldx < currentChart1MJArrayTrx.Count(); nldx++)
            {
                //Console.WriteLine(currentChart1MJArrayBtc[nldx]["candle_date_time_kst"]);
                priceInfoList.Add(new PriceInfoEntityObject()
                {
                    일자 = DateTime.Parse(currentChart1MJArrayBtc[currentChart1MJArrayTrx.Count() - 1 - nldx]["candle_date_time_kst"].ToString()).ToOADate(),
                    시가 = Convert.ToInt32(Double.Parse(currentChart1MJArrayBtc[currentChart1MJArrayTrx.Count() - 1 - nldx]["opening_price"].ToString())),
                    고가 = Convert.ToInt32(Double.Parse(currentChart1MJArrayBtc[currentChart1MJArrayTrx.Count() - 1 - nldx]["high_price"].ToString())),
                    저가 = Convert.ToInt32(Double.Parse(currentChart1MJArrayBtc[currentChart1MJArrayTrx.Count() - 1 - nldx]["low_price"].ToString())),
                    종가 = Convert.ToInt32(Double.Parse(currentChart1MJArrayBtc[currentChart1MJArrayTrx.Count() - 1 - nldx]["trade_price"].ToString())),
                    거래량 = Convert.ToInt32(Double.Parse(currentChart1MJArrayBtc[currentChart1MJArrayTrx.Count() - 1 - nldx]["candle_acc_trade_volume"].ToString()))
                });

                if (this.InvokeRequired)
                {

                    this.Invoke(new Action(() =>
                    {
                        chart1.Series["sCandle"].Points.AddXY(priceInfoList[nldx].일자, priceInfoList[nldx].고가);
                        chart1.Series["sCandle"].Points[nldx].YValues[1] = priceInfoList[nldx].저가;
                        chart1.Series["sCandle"].Points[nldx].YValues[2] = priceInfoList[nldx].시가;
                        chart1.Series["sCandle"].Points[nldx].YValues[3] = priceInfoList[nldx].종가;
                        chart1.Series["sVolume"].Points.AddXY(priceInfoList[nldx].일자, priceInfoList[nldx].거래량);
                    }));

                    if (priceInfoList[nldx].시가 > priceInfoList[nldx].종가)
                    {
                        this.Invoke(new Action(() =>
                        {
                            chart1.Series["sCandle"].Points[nldx].Color = Color.Blue;
                            chart1.Series["sVolume"].Points[nldx].Color = Color.Blue;
                        }));
                    }
                    else
                    {
                        this.Invoke(new Action(() =>
                        {
                            chart1.Series["sCandle"].Points[nldx].Color = Color.Red;
                            chart1.Series["sVolume"].Points[nldx].Color = Color.Red;
                        }));
                    }
                }
                else
                {
                    chart1.Series["sCandle"].Points.AddXY(priceInfoList[nldx].일자, priceInfoList[nldx].고가);
                    chart1.Series["sCandle"].Points[nldx].YValues[1] = priceInfoList[nldx].저가;
                    chart1.Series["sCandle"].Points[nldx].YValues[2] = priceInfoList[nldx].시가;
                    chart1.Series["sCandle"].Points[nldx].YValues[3] = priceInfoList[nldx].종가;
                    chart1.Series["sVolume"].Points.AddXY(priceInfoList[nldx].일자, priceInfoList[nldx].거래량);

                    if (priceInfoList[nldx].시가 > priceInfoList[nldx].종가)
                    {
                        chart1.Series["sCandle"].Points[nldx].Color = Color.Blue;
                        chart1.Series["sVolume"].Points[nldx].Color = Color.Blue;
                    }
                    else
                    {
                        chart1.Series["sCandle"].Points[nldx].Color = Color.Red;
                        chart1.Series["sVolume"].Points[nldx].Color = Color.Red;
                    }
                }

                double max = 0;
                double min = 0;

                min = priceInfoList[0].저가;
                max = priceInfoList[0].고가;
                for (int i = 0; i < priceInfoList.Count; i++)
                {

                    if (priceInfoList[i].고가 > max)
                        max = priceInfoList[i].고가;
                    if (priceInfoList[i].저가 < min)
                        min = priceInfoList[i].저가;
                }

                if (this.InvokeRequired)
                {
                    this.Invoke(new Action(() =>
                    {
                        this.chart1.ChartAreas[0].AxisY.Maximum = max * 1.001;
                        this.chart1.ChartAreas[0].AxisY.Minimum = min * 0.999;
                    }));
                }
                else
                {
                    this.chart1.ChartAreas[0].AxisY.Maximum = max * 1.001;
                    this.chart1.ChartAreas[0].AxisY.Minimum = min * 0.999;
                }
            }
        }

        private void updateDrawChart()
        {
            int lastChartIndex = chart1.Series["sCandle"].Points.Count() - 1;
            Up U = new Up("W5zjjBBit8GjGd2mY5rOKFuVY12HV17q513qlmdr", "igQC32jg0NBMMUWadSs39iIAWft7sBGiaL801Unx");

            priceInfoList = new List<PriceInfoEntityObject>();

            JArray currentChart1MJArrayTrx;
            JArray currentChart1MJArrayBtc;

            string json_candle_1m_trx = U.GetCandles_Minute("KRW-TRX", Up.UpbitMinuteCandleType._1, to: DateTime.Now.AddMinutes(0), count: 1);
            string json_candle_1m_btc = U.GetCandles_Minute("KRW-BTC", Up.UpbitMinuteCandleType._1, to: DateTime.Now.AddMinutes(0), count: 1);

            currentChart1MJArrayTrx = JArray.Parse(json_candle_1m_trx);
            currentChart1MJArrayBtc = JArray.Parse(json_candle_1m_btc);

            priceInfoList.Add(new PriceInfoEntityObject()
            {
                일자 = DateTime.Parse(currentChart1MJArrayBtc[0]["candle_date_time_kst"].ToString()).ToOADate(),
                시가 = Convert.ToInt32(Double.Parse(currentChart1MJArrayBtc[0]["opening_price"].ToString())),
                고가 = Convert.ToInt32(Double.Parse(currentChart1MJArrayBtc[0]["high_price"].ToString())),
                저가 = Convert.ToInt32(Double.Parse(currentChart1MJArrayBtc[0]["low_price"].ToString())),
                종가 = Convert.ToInt32(Double.Parse(currentChart1MJArrayBtc[0]["trade_price"].ToString())),
                거래량 = Convert.ToInt32(Double.Parse(currentChart1MJArrayBtc[0]["candle_acc_trade_volume"].ToString()))
            });

            // 차트 업데이트
            // 현재 시간과 같으면 마지막 x 차트에 값만 업데이트
            if (prevLastTime.Equals(priceInfoList[0].일자))
            {
                if (this.InvokeRequired)
                {
                    this.Invoke(new Action(() =>
                    {
                        chart1.Series["sCandle"].Points[lastChartIndex].YValues[0] = priceInfoList[0].고가;
                        chart1.Series["sCandle"].Points[lastChartIndex].YValues[1] = priceInfoList[0].저가;
                        chart1.Series["sCandle"].Points[lastChartIndex].YValues[2] = priceInfoList[0].시가;
                        chart1.Series["sCandle"].Points[lastChartIndex].YValues[3] = priceInfoList[0].종가;
                        chart1.Series["sVolume"].Points[lastChartIndex].YValues[0] = priceInfoList[0].거래량;
                    }));

                    if (priceInfoList[0].시가 > priceInfoList[0].종가)
                    {
                        this.Invoke(new Action(() =>
                        {
                            chart1.Series["sCandle"].Points[lastChartIndex].Color = Color.Blue;
                            chart1.Series["sVolume"].Points[lastChartIndex].Color = Color.Blue;
                        }));
                    }
                    else
                    {
                        this.Invoke(new Action(() =>
                        {
                            chart1.Series["sCandle"].Points[lastChartIndex].Color = Color.Red;
                            chart1.Series["sVolume"].Points[lastChartIndex].Color = Color.Red;
                        }));
                    }
                }
                else
                {
                    chart1.Series["sCandle"].Points[lastChartIndex].YValues[0] = priceInfoList[0].고가;
                    chart1.Series["sCandle"].Points[lastChartIndex].YValues[1] = priceInfoList[0].저가;
                    chart1.Series["sCandle"].Points[lastChartIndex].YValues[2] = priceInfoList[0].시가;
                    chart1.Series["sCandle"].Points[lastChartIndex].YValues[3] = priceInfoList[0].종가;
                    chart1.Series["sVolume"].Points[lastChartIndex].YValues[0] = priceInfoList[0].거래량;

                    if (priceInfoList[0].시가 > priceInfoList[0].종가)
                    {
                        chart1.Series["sCandle"].Points[lastChartIndex].Color = Color.Blue;
                        chart1.Series["sVolume"].Points[lastChartIndex].Color = Color.Blue;
                    }
                    else
                    {
                        chart1.Series["sCandle"].Points[lastChartIndex].Color = Color.Red;
                        chart1.Series["sVolume"].Points[lastChartIndex].Color = Color.Red;
                    }
                }

                int max = 0;
                int min = 0;

                min = (int)Int32.Parse(chart1.Series["sCandle"].Points[0].YValues[1].ToString());
                max = (int)Int32.Parse(chart1.Series["sCandle"].Points[0].YValues[0].ToString());
                for (int xCount = 0; xCount < (chart1.Series["sCandle"].Points.Count()); xCount++)
                {
                    if (Int32.Parse(chart1.Series["sCandle"].Points[xCount].YValues[0].ToString()) > max)
                        max = (int)Int32.Parse(chart1.Series["sCandle"].Points[xCount].YValues[0].ToString());
                    if (Int32.Parse(chart1.Series["sCandle"].Points[xCount].YValues[1].ToString()) < min)
                        min = (int)Int32.Parse(chart1.Series["sCandle"].Points[xCount].YValues[1].ToString());
                }

                if (this.InvokeRequired)
                {
                    this.Invoke(new Action(() =>
                    {
                        this.chart1.ChartAreas[0].AxisY.Maximum = max * 1.001;
                        this.chart1.ChartAreas[0].AxisY.Minimum = min * 0.999;
                    }));
                }
                else
                {
                    this.chart1.ChartAreas[0].AxisY.Maximum = max * 1.001;
                    this.chart1.ChartAreas[0].AxisY.Minimum = min * 0.999;
                }

            }
            // 현재 시간과 같지 않으면 x 차트 추가
            else
            {
                prevLastTime = priceInfoList[0].일자;

                if (this.InvokeRequired)
                {
                    this.Invoke(new Action(() =>
                    {
                        chart1.Series["sCandle"].Points.AddXY(priceInfoList[0].일자, priceInfoList[0].고가);
                        chart1.Series["sCandle"].Points[lastChartIndex+1].YValues[1] = priceInfoList[0].저가;
                        chart1.Series["sCandle"].Points[lastChartIndex+1].YValues[2] = priceInfoList[0].시가;
                        chart1.Series["sCandle"].Points[lastChartIndex+1].YValues[3] = priceInfoList[0].종가;
                        chart1.Series["sVolume"].Points.AddXY(priceInfoList[0].일자, priceInfoList[0].거래량);
                        chart1.Series["sCandle"].Points.RemoveAt(0);
                        chart1.Series["sVolume"].Points.RemoveAt(0);
                        chart1.Series["sRSI"].Points.RemoveAt(0);
                        chart1.Series["sBB"].Points.RemoveAt(0);
                        chart1.Series["sMovingAverage"].Points.RemoveAt(0);
                        chart1.Series["sStochastic"].Points.RemoveAt(0);
                        chart1.Series["sSMA"].Points.RemoveAt(0);
                        chart1.Series["sCCI"].Points.RemoveAt(0);
                        chart1.Series["sMFI"].Points.RemoveAt(0);
                        chart1.ResetAutoValues();
                    }));

                    if (priceInfoList[0].시가 > priceInfoList[0].종가)
                    {
                        this.Invoke(new Action(() =>
                        {
                            chart1.Series["sCandle"].Points[lastChartIndex].Color = Color.Blue;
                            chart1.Series["sVolume"].Points[lastChartIndex].Color = Color.Blue;
                        }));
                    }
                    else
                    {
                        this.Invoke(new Action(() =>
                        {
                            chart1.Series["sCandle"].Points[lastChartIndex].Color = Color.Red;
                            chart1.Series["sVolume"].Points[lastChartIndex].Color = Color.Red;
                        }));
                    }
                }
                else
                {

                    chart1.Series["sCandle"].Points.AddXY(priceInfoList[0].일자, priceInfoList[0].고가);
                    chart1.Series["sCandle"].Points[lastChartIndex].YValues[1] = priceInfoList[0].저가;
                    chart1.Series["sCandle"].Points[lastChartIndex].YValues[2] = priceInfoList[0].시가;
                    chart1.Series["sCandle"].Points[lastChartIndex].YValues[3] = priceInfoList[0].종가;
                    chart1.Series["sVolume"].Points.AddXY(priceInfoList[0].일자, priceInfoList[0].거래량);
                    chart1.Series["sCandle"].Points.RemoveAt(0);
                    chart1.Series["sVolume"].Points.RemoveAt(0);
                    chart1.Series["sRSI"].Points.RemoveAt(0);
                    chart1.Series["sBB"].Points.RemoveAt(0);
                    chart1.Series["sMovingAverage"].Points.RemoveAt(0);
                    chart1.Series["sStochastic"].Points.RemoveAt(0);
                    chart1.Series["sSMA"].Points.RemoveAt(0);
                    chart1.Series["sCCI"].Points.RemoveAt(0);
                    chart1.Series["sMFI"].Points.RemoveAt(0);
                    chart1.ResetAutoValues();

                    if (priceInfoList[0].시가 > priceInfoList[0].종가)
                    {
                        chart1.Series["sCandle"].Points[lastChartIndex].Color = Color.Blue;
                        chart1.Series["sVolume"].Points[lastChartIndex].Color = Color.Blue;
                    }
                    else
                    {
                        chart1.Series["sCandle"].Points[lastChartIndex].Color = Color.Red;
                        chart1.Series["sVolume"].Points[lastChartIndex].Color = Color.Red;
                    }
                }
                //Console.WriteLine("2");
                int max = 0;
                int min = 0;

                min = (int)Int32.Parse(chart1.Series["sCandle"].Points[0].YValues[1].ToString());
                max = (int)Int32.Parse(chart1.Series["sCandle"].Points[0].YValues[0].ToString());
                for (int xCount = 0; xCount < (chart1.Series["sCandle"].Points.Count()) - 1; xCount++)
                {
                    if (Int32.Parse(chart1.Series["sCandle"].Points[xCount].YValues[0].ToString()) > max)
                        max = (int)Int32.Parse(chart1.Series["sCandle"].Points[xCount].YValues[0].ToString());
                    if (Int32.Parse(chart1.Series["sCandle"].Points[xCount].YValues[1].ToString()) < min)
                        min = (int)Int32.Parse(chart1.Series["sCandle"].Points[xCount].YValues[1].ToString());
                }
                //Console.WriteLine("3," + min + ", " + max);
                if (this.InvokeRequired)
                {
                    this.Invoke(new Action(() =>
                    {
                        this.chart1.ChartAreas[0].AxisY.Maximum = max * 1.001;
                        this.chart1.ChartAreas[0].AxisY.Minimum = min * 0.999;
                    }));
                }
                else
                {
                    this.chart1.ChartAreas[0].AxisY.Maximum = max * 1.001;
                    this.chart1.ChartAreas[0].AxisY.Minimum = min * 0.999;
                }
                //Console.WriteLine("4");
            }

            // BTC 급락 분석
            if (priceInfoList[0].시가 > priceInfoList[0].종가)
            {
                if (SELL_PRICE < (priceInfoList[0].시가 - priceInfoList[0].종가))
                {
                    if (this.InvokeRequired)
                    {
                        this.Invoke(new Action(() =>
                        {
                            rtbWarning.Clear();
                            this.rtbWarning.SelectionColor = Color.Blue;
                            this.rtbWarning.AppendText("S" + "\r\n");
                            this.rtbWarning.SelectionColor = Color.Blue;
                            this.rtbWarning.AppendText("- " + String.Format("{0:#,0}", priceInfoList[0].시가 - priceInfoList[0].종가));
                        }));
                    }
                    else
                    {
                        this.rtbWarning.Text = "S";
                    }
                }
                else
                {
                    if (this.InvokeRequired)
                    {
                        this.Invoke(new Action(() =>
                        {
                            rtbWarning.Clear();
                            this.rtbWarning.SelectionColor = Color.Black;
                            this.rtbWarning.AppendText("-" + "\r\n");
                            this.rtbWarning.AppendText("- " + String.Format("{0:#,0}", priceInfoList[0].시가 - priceInfoList[0].종가));
                        }));
                    }
                    else
                    {
                        this.rtbWarning.Text = "-";
                    }
                }

            }
            else
            {
                if (this.InvokeRequired)
                {
                    this.Invoke(new Action(() =>
                    {
                        rtbWarning.Clear();
                        this.rtbWarning.SelectionColor = Color.Red;
                        this.rtbWarning.AppendText("B" + "\r\n");
                        this.rtbWarning.SelectionColor = Color.Red;
                        this.rtbWarning.AppendText("+ " + String.Format("{0:#,0}", priceInfoList[0].종가 - priceInfoList[0].시가));
                    }));
                }
                else
                {
                    this.rtbWarning.Text = "B";
                }
            }
        }

#endregion

        public int convertStringToInto(string str)
        {
            int convertIntValue = (int)float.Parse(str);
            return convertIntValue;
        }

        private void chart1_AxisViewChanged(object sender, ViewEventArgs e)
        {
            if (sender.Equals(chart1))
            {
                int startPosition = (int)e.Axis.ScaleView.ViewMinimum;
                int endPosition = (int)e.Axis.ScaleView.ViewMaximum;

                int max = (int)e.ChartArea.AxisY.ScaleView.ViewMinimum;
                int min = (int)e.ChartArea.AxisY.ScaleView.ViewMaximum;

                for (int xCount = startPosition - 1; xCount < endPosition; xCount++)
                {
                    if (xCount >= chart1.Series["sCandle"].Points.Count())
                        break;
                    if (xCount < 0)
                        xCount = 0;

                    if (Int32.Parse(chart1.Series["sCandle"].Points[xCount].YValues[0].ToString()) > max)
                        max = (int)Int32.Parse(chart1.Series["sCandle"].Points[xCount].YValues[0].ToString());
                    if (Int32.Parse(chart1.Series["sCandle"].Points[xCount].YValues[1].ToString()) < min)
                        min = (int)Int32.Parse(chart1.Series["sCandle"].Points[xCount].YValues[1].ToString());

                    Console.WriteLine(" xCount [{0}] : {1}", xCount, chart1.Series["sCandle"].Points[xCount].YValues[1].ToString());
                }

                this.chart1.ChartAreas[0].AxisY.Maximum = max;
                this.chart1.ChartAreas[0].AxisY.Minimum = min;
            }
        }

        /// <summary>
        /// This method calculates a different indicator if corresponding
        /// item in the combo box is selected.
        /// </summary>
        private void Calculations()
        {
            //Console.WriteLine("Calculations");

            // Relative Strength Index
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() =>
                {
                    this.chart1.DataManipulator.FinancialFormula(FinancialFormula.RelativeStrengthIndex, "10", "sCandle:Y4", "sRSI");
                    chart1.ChartAreas["caRSI"].AxisY.Minimum = 0;
                    chart1.ChartAreas["caRSI"].AxisY.Maximum = 100;

                    StripLine stripLine = new StripLine();
                    chart1.ChartAreas["caRSI"].AxisY.StripLines.Add(stripLine);
                    stripLine.Interval = 70;
                    stripLine.StripWidth = 30;
                    stripLine.BackColor = Color.FromArgb(64, 200, 191, 228);

                    chart1.ChartAreas["caRSI"].AxisX.Minimum = chart1.Series["sCandle"].Points[0].XValue;
                }));
            }

            // Bollinger Bands
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() =>
                {
                    // Bollinger Bands - moving average
                    chart1.DataManipulator.FinancialFormula(FinancialFormula.MovingAverage, "10", "sCandle:Y", "sMovingAverage");
                    // Bollinger Bands - upper, lower
                    chart1.DataManipulator.FinancialFormula(FinancialFormula.BollingerBands, "10,2", "sCandle:Y", "sBB,sBB:Y2");
                }));
            }

            // Stochastic
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() =>
                {
                    chart1.DataManipulator.FinancialFormula(FinancialFormula.StochasticIndicator, "5,5", "sCandle:Y,sCandle:Y2,sCandle:Y4", "sStochastic,sSMA");

                    StripLine stripLine = new StripLine();
                    chart1.ChartAreas["caStochastic"].AxisY.StripLines.Add(stripLine);
                    stripLine.Interval = 70;
                    stripLine.StripWidth = 30;
                    // stripLine.BackColor = Color.FromArgb(64, 165, 191, 228);
                    stripLine.BackColor = Color.FromArgb(64, 200, 191, 228);

                    chart1.ChartAreas["caStochastic"].AxisX.Minimum = chart1.Series["sCandle"].Points[0].XValue;
                }));
            }

            // Commodity Channel Index
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() =>
                {
                    chart1.DataManipulator.FinancialFormula(FinancialFormula.CommodityChannelIndex, "10","sCandle:Y,sCandle:Y2,sCandle:Y4", "sCCI");

                    //StripLine stripLine = new StripLine();
                    //chart1.ChartAreas["caCCI"].AxisY.StripLines.Add(stripLine);
                    //stripLine.Interval = 100;
                    //stripLine.StripWidth = 30;
                    //stripLine.BackColor = Color.FromArgb(64, 200, 191, 228);

                    chart1.ChartAreas["caCCI"].AxisX.Minimum = chart1.Series["sCandle"].Points[0].XValue;
                }));
            }

            // Money Flow Indicator
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() =>
                {
                    chart1.DataManipulator.FinancialFormula(FinancialFormula.MoneyFlow, "10", "sCandle:Y,sCandle:Y2,sCandle:Y4, sVolume:Y", "sMFI");

                    //StripLine stripLine = new StripLine();
                    //chart1.ChartAreas["caCCI"].AxisY.StripLines.Add(stripLine);
                    //stripLine.Interval = 100;
                    //stripLine.StripWidth = 30;
                    //stripLine.BackColor = Color.FromArgb(64, 200, 191, 228);

                    chart1.ChartAreas["caMFI"].AxisX.Minimum = chart1.Series["sCandle"].Points[0].XValue;
                }));
            }

            // Draw chart1
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() =>
                {
                    chart1.Invalidate();

                }));
            }

            //
            int lastChartIndex = chart1.Series["sCandle"].Points.Count() - 1;
            double close_price = chart1.Series["sCandle"].Points[lastChartIndex].YValues[3];
            double rsi_value = chart1.Series["sRSI"].Points[lastChartIndex - 10].YValues[0];
            double bb_upper_price = chart1.Series["sBB"].Points[lastChartIndex - 9].YValues[0];
            double bb_moving_average_price_prev = chart1.Series["sMovingAverage"].Points[lastChartIndex - 10].YValues[0];
            double bb_moving_average_price = chart1.Series["sMovingAverage"].Points[lastChartIndex - 9].YValues[0];
            double bb_lower_price = chart1.Series["sBB"].Points[lastChartIndex - 9].YValues[1];
            double cci_value = chart1.Series["sCCI"].Points[lastChartIndex - 9].YValues[0];
            double stochastic_k_prev = chart1.Series["sStochastic"].Points[lastChartIndex - 9].YValues[0];
            double stochastic_d_prev = chart1.Series["sSMA"].Points[lastChartIndex - 9].YValues[0];
            double stochastic_k = chart1.Series["sStochastic"].Points[lastChartIndex - 8].YValues[0];
            double stochastic_d = chart1.Series["sSMA"].Points[lastChartIndex - 8].YValues[0];
            double mfi_value = chart1.Series["sMFI"].Points[lastChartIndex - 9].YValues[0];
            //Console.WriteLine(" -------------- " + tmp_count++);
            //Console.WriteLine(" Close [{0}]", close_price);
            //Console.WriteLine(" RSI [{0}]", rsi_value);
            //Console.WriteLine(" BB [{0}], [{1}], [{2}]", bb_upper_price, bb_moving_average_price, bb_lower_price);
            //Console.WriteLine(" CCI [{0}]", cci_value);
            //Console.WriteLine(" Stochastic [{0}], [{1}]", stochastic_k, stochastic_d);
            //Console.WriteLine(" MFI [{0}]",  mfi_value);
            //Console.WriteLine(" -------------- ");

            //
            if(bb_moving_average_price_prev < bb_moving_average_price)
            {
                Console.WriteLine(" High " + mfi_value + " , " + stochastic_k + " , " + stochastic_d + " , " + isBuy);
                if(mfi_value > 70)
                {
                    //Console.WriteLine(": Sell ");
                    //if(stochastic_k < stochastic_d)
                    if (stochastic_k_prev < stochastic_d_prev)
                    {
                        if(isBuy == true)
                        {
                            Console.WriteLine(" -------------- ");
                            Console.WriteLine(DateTime.Now.ToString("[yy-MM-dd HH:mm:ss] ") + "Sell BT: " + buy_price + " , " + close_price);
                            Console.WriteLine(DateTime.Now.ToString("[yy-MM-dd HH:mm:ss] ") + "Sell TRX: " + buy_price_to_alt + " , " + trx_price);
                            Console.WriteLine(" -------------- ");
                            isBuy = false;
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine(" Low : " + mfi_value + " , " + stochastic_k + " , " + stochastic_d + " , " + isBuy);
                if(mfi_value < 30)
                {
                    //Console.WriteLine(": Buy ");
                    //if (stochastic_k > stochastic_d)
                    if (stochastic_k_prev > stochastic_d_prev)
                    {
                        if (isBuy == false)
                        {
                            Console.WriteLine(" -------------- ");
                            Console.WriteLine(DateTime.Now.ToString("[yy-MM-dd HH:mm:ss] ") + "Buy BT: " + close_price);
                            Console.WriteLine(DateTime.Now.ToString("[yy-MM-dd HH:mm:ss] ") + "Buy TRX: " + trx_price);
                            Console.WriteLine(" -------------- ");
                            isBuy = true;
                            buy_price = close_price;
                            buy_price_to_alt = trx_price;
                        }
                    }
                }
            }
#if true
            if ((isBuy == true) && (close_price < (buy_price * 0.99)))
            {
                Console.WriteLine(" -------------- ");
                Console.WriteLine(DateTime.Now.ToString("[yy-MM-dd HH:mm:ss] ") + "Force Sell BT: " + buy_price + " , " + close_price);
                Console.WriteLine(DateTime.Now.ToString("[yy-MM-dd HH:mm:ss] ") + "Force Sell TRX: " + buy_price_to_alt + " , " + trx_price);
                Console.WriteLine(" -------------- ");
                isBuy = false;
            }
#endif
        }
    }

    class PriceInfoEntityObject
    {
        public double 일자 { get; set; }
        public int 시가 { get; set; }
        public int 고가 { get; set; }
        public int 저가 { get; set; }
        public int 종가 { get; set; }
        public int 거래량 { get; set; }
    }


}
