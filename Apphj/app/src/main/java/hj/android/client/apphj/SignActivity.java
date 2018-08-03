package hj.android.client.apphj;
import android.content.BroadcastReceiver;
import android.content.Context;
import android.content.Intent;
import android.content.IntentFilter;
import android.graphics.Typeface;
import android.location.LocationManager;
import android.os.Bundle;
import android.provider.Settings;
import android.support.v7.app.AppCompatActivity;
import android.view.View;
import android.widget.EditText;
import android.widget.ImageView;
import android.widget.TextView;
import android.widget.Toast;

public class SignActivity extends AppCompatActivity {

    public static aStore VarStore = new aStore();


    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);






        final LocationManager manager = (LocationManager) getSystemService( Context.LOCATION_SERVICE );

        if ( !manager.isProviderEnabled( LocationManager.GPS_PROVIDER ) ) {
            String locationProviders = Settings.Secure.getString(getContentResolver(), Settings.Secure.LOCATION_PROVIDERS_ALLOWED);
            if (locationProviders == null || locationProviders.equals("")) {
                startActivity(new Intent(Settings.ACTION_LOCATION_SOURCE_SETTINGS));
            }

        }




        if (VarStore.get(getApplicationContext() , "OK") == "1"){
           setContentView(R.layout.activity_main);
        }else{
            setContentView(R.layout.signup_main);
        }

        mIntentFilter = new IntentFilter();
        mIntentFilter.addAction(mBroadcastStringAction);

    }





    public void sv(View v){
        EditText txtN = (EditText)findViewById(R.id.editTextN);
        EditText txtP = (EditText)findViewById(R.id.editTextP);




        if (txtN.getText().toString().trim().length() == 0){
            Toast.makeText(getApplicationContext(),"0- Empty", Toast.LENGTH_SHORT).show();
            return;
        }
        if (txtP.getText().toString().trim().length() == 0){
            Toast.makeText(getApplicationContext(),"1- Empty", Toast.LENGTH_SHORT).show();
            return;
        }








        VarStore.save(getApplicationContext(),txtN.getText().toString(),"txtN");
        VarStore.save(getApplicationContext(),txtP.getText().toString(),"txtP");



            try {
                if (VarStore.IsRunning(aServiceSocket.class, getApplicationContext()) == false) {
                    Intent aIntent = new Intent(this, aServiceSocket.class);
                    startService(aIntent);
                }else {
                    if (aServiceSocket.client != null){
                        if(aServiceSocket.outputStream!=null){
                            String n,p;
                            n= "";
                            p= "";
                            if (VarStore.get(getApplicationContext() , "txtN") != ""){
                                n = VarStore.get(getApplicationContext() , "txtN").toString();
                            }
                            if (VarStore.get(getApplicationContext() , "txtP") != ""){
                                p = VarStore.get(getApplicationContext() , "txtP").toString();
                            }

                            aServiceSocket.Send("L" + aServiceSocket.spl_d + n + aServiceSocket.spl_d + p);
                        }
                    }
                }
            } catch (Exception e) {}



    }





    private IntentFilter mIntentFilter;
    public static final String mBroadcastStringAction = "com.m.broadcast.m";
    private BroadcastReceiver mReceiver = new BroadcastReceiver() {

        @Override
        public void onReceive(Context context, Intent intent) {
            if (intent.getAction().equals(mBroadcastStringAction)) {
                String str =  intent.getStringExtra("Data");

               try{
                   if(str != null){
                       if(str.length() != 0){
                           TextView textview = (TextView)findViewById(R.id.textView2);

                           textview.setText(str) ;

                           textview.setVisibility(View.VISIBLE);


                       }
                   }
               } catch (Exception e) {

               }





            }
        }
    };
    @Override
    public void onResume() {
        super.onResume();
       registerReceiver(mReceiver, mIntentFilter);
    }
















}
