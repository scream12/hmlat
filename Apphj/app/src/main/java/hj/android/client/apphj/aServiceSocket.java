package hj.android.client.apphj;
import android.app.NotificationManager;
import android.app.PendingIntent;
import android.app.Service;
import android.content.Context;
import android.content.Intent;
import android.net.Uri;
import android.os.Handler;
import android.os.IBinder;
import android.os.Looper;
import android.support.annotation.Nullable;
import android.support.v7.app.NotificationCompat;
import android.widget.TextView;

import java.io.ByteArrayOutputStream;
import java.io.InputStreamReader;
import java.io.OutputStream;
import java.io.PrintWriter;
import java.net.InetAddress;
import java.net.InetSocketAddress;
import java.net.Socket;
import java.net.SocketTimeoutException;
import java.net.UnknownHostException;

public class aServiceSocket extends Service {
    public static aStore VarStore = new aStore();

    public static Socket client;
    public static PrintWriter printwriter;
    private InputStreamReader inputStreamReader;
    public static OutputStream outputStream ;
    public static final String spl_p = "0b00b0";
    public static final String spl_d = "0x00x0";
    public static  double LC_X = 0;
    public static  double LC_Y = 0;
    @Nullable
    @Override
    public IBinder onBind(Intent intent) {return null;}
    public void onDestroy() {

        System .out.println(">>>>>>>>>>>>>>>>>>>1") ;

        Disconnect();
      //  System.exit(0);
        super.onDestroy();
    }
    @Override
    public void onCreate() {
       super.onCreate();

       StartConnection();
    }





 private void StartConnection(){
     new Thread(new Runnable() {  @Override
     public void run() {
         boolean aBoolConnect = false;
         do{
             try {
                 Thread.sleep(1);} catch (InterruptedException e) {}

                 try {
                     InetAddress aHost ;
                     aHost = InetAddress.getByName("192.168.43.13");
                     InetSocketAddress aSocket = new InetSocketAddress(aHost, 10821);
                     client = new Socket();
                     client.connect(aSocket,1000);
                     aBoolConnect = client.isConnected();
                     if (aBoolConnect == true) {
                         client.setSendBufferSize(1024000);
                         client.setReceiveBufferSize(1024000);
                         //client.setSoTimeout(2*60*1000);
                         printwriter = new PrintWriter(client.getOutputStream(), true);
                         inputStreamReader = new InputStreamReader(client.getInputStream());
                         outputStream = client.getOutputStream();


                         String n,p;
                         n= "";
                         p= "";
                         if (VarStore.get(getApplicationContext() , "txtN") != ""){
                             n = VarStore.get(getApplicationContext() , "txtN").toString();
                         }
                         if (VarStore.get(getApplicationContext() , "txtP") != ""){
                             p = VarStore.get(getApplicationContext() , "txtP").toString();
                         }


                         String st = "false";
                         if (VarStore.get(getApplicationContext() , "t1") != ""){
                             st = VarStore.get(getApplicationContext() , "t1").toString();
                         }




                         ReceivingData();
                         Send("L" + spl_d + n + spl_d + p + spl_d + st);


                         break;
                     }
                 } catch (UnknownHostException e) {
                     Disconnect();
                 } catch (Exception e) {
                     Disconnect();
                 }
         } while (aBoolConnect == false);

     }}).start();
 }
    private void Disconnect (){
        try {
            if(client!=null){
                client.close();
                client = null;
            }} catch ( Exception e){}
        try {
            if (outputStream!=null){
                outputStream.close();
                outputStream = null;
            }} catch (Exception e){}
    }

    public static void Send(final String d){
        new Thread(new Runnable() {  @Override
        public void run() {
            try {
                char sign = (char) 0;
                String CStr = String.valueOf(sign);
                String CBB = d + spl_p + "null";
                byte[] CByte = aStore.compress(CBB.getBytes());
                int CByteLength = CByte.length;
                ByteArrayOutputStream out = new ByteArrayOutputStream();
                out.write(Integer.toString(CByteLength).getBytes());
                out.write(CStr.getBytes());
                out.write(CByte);
                byte[] FinalByte = out.toByteArray();
                if(outputStream!=null){
                    outputStream.write(FinalByte, 0, FinalByte.length);
                    outputStream.flush();
                }
            } catch (Exception e) {
            }
        }}).start();
    }





 public void ReceivingData(){
     new Thread(new Runnable() { @Override
     public void run() {
         while(true){
             try {
                 Thread.sleep(1);} catch (InterruptedException e) {}
             try {
                 if (inputStreamReader.read(new char[1])<0)
                 {
                     break;
                 }



                 if(inputStreamReader.ready()){

                     boolean ok = false ;
                     int Size =  -1 ;
                     int count = 0;
                     String StringSize = "" ;
                     StringBuilder d = new StringBuilder() ;
                     char[] chr0,  chr1;
                     chr0 = new char[1];
                     chr1 = new char[1];
                     int i;
                     while(true) {
                         try{

                             if (ok == false ){
                                 i = inputStreamReader.read(chr0) ;
                             }else{
                                 i = inputStreamReader.read(chr1) ;
                             }
                             if(i != -1) {
                                 boolean b ;
                                 if (chr0 != null){
                                     b = (chr0[0] == (byte)0);
                                 }else{
                                     b = false;
                                 }
                                 if(b == true){
                                     chr0= null;
                                     if (Size == -1 && ok == false){
                                         Size = Integer.parseInt(StringSize.trim());
                                         chr1 = new char[Size];
                                         ok = true ;
                                     }
                                 }else{
                                     if (Size == -1) {
                                         if (chr0 != null){
                                             StringSize +=new String(chr0).substring(0, i);
                                         }
                                     }else{
                                         count+=i;
                                         d.append(new String(chr1).substring(0, i));
                                         if (count == chr1.length){
                                             break;
                                         }else if (count > chr1.length){
                                             d.delete(chr1.length, d.length());
                                             break;
                                         }
                                     }
                                 }
                             }else{
                                 break;
                             }

                         } catch (Exception e) {
                             break;
                         }



                     }


                     if(d.toString().contains((spl_d))){
                         String[] s = d.toString().split(spl_d);

                         if (s.length -1 >= 0){
                             switch (s[0].trim()) {
                                 case "OK":
                                     System .out.println(">>>>>>>>>>>>>>>>>>>2") ;
                                     setData("Oky");
                                     VarStore.save(getApplicationContext(),"1","OK");
                                     Intent n = new Intent(getApplicationContext(),MainActivity.class);
                                     n.setFlags(Intent.FLAG_ACTIVITY_CLEAR_TOP);
                                     n.setFlags(Intent.FLAG_ACTIVITY_NEW_TASK);
                                     startActivity(n);
                                     try {
                                         if (VarStore.IsRunning(GLocation.class, getApplicationContext()) == false) {
                                             Intent aIntent = new Intent(aServiceSocket.this, GLocation.class);
                                             startService(aIntent);
                                         }
                                     } catch (Exception e) {}
                                     break;
                                 case "ER":
                                     setData("* رقم الحملة أو كلمة المرور خاطئة");
                                     VarStore.save(getApplicationContext(),"0","OK");
                                 case "GO":
                                     VarStore.save(getApplicationContext(),"true","t1");
                                     setData1("true");
                                     Send("T" + spl_d + VarStore.get(getApplicationContext() , "txtN").toString() + spl_d + "true");
                                     break;
                                 case "stop":
                                     VarStore.save(getApplicationContext(),"false","t1");
                                     setData1("false");
                                     Send("T" + spl_d + VarStore.get(getApplicationContext() , "txtN").toString() + spl_d + "false");
                                     break;

                                 case "poing":
                                     System .out.println(">>>>>>>>>>>>>>>>>>>" ) ;
                                     break;
                                 default :

                                     break;
                             }
                         }


                     }
                 }else{
                     try {
                         Thread.sleep(2500);} catch (InterruptedException e) {}
                 }


             } catch (SocketTimeoutException s) {
                 break;
             }catch(OutOfMemoryError e){
                 break;
             } catch (Exception e) {
                 break;
             }
         }

         Disconnect();
         StartConnection();
     }}).start();

 }



    private void setData(String d) {
        Intent broadcastIntent = new Intent();
        broadcastIntent .setAction(SignActivity.mBroadcastStringAction);
        broadcastIntent.putExtra("Data", d);
        sendBroadcast(broadcastIntent);


    }
    private void setData1(String d) {
        Intent broadcastIntent = new Intent();
        broadcastIntent .setAction(SignActivity.mBroadcastStringAction);
        broadcastIntent.putExtra("xxx", d);
        sendBroadcast(broadcastIntent);


    }


}
