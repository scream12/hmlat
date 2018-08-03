package hj.android.client.apphj;

import android.content.BroadcastReceiver;
import android.content.Context;
import android.content.Intent;
import android.content.IntentFilter;
import android.os.Bundle;
import android.support.design.widget.NavigationView;
import android.support.v4.view.GravityCompat;
import android.support.v4.widget.DrawerLayout;
import android.support.v7.app.ActionBarDrawerToggle;
import android.support.v7.app.AppCompatActivity;
import android.support.v7.widget.Toolbar;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.widget.ImageView;
import android.widget.TextView;
import android.widget.Toast;


public class Activity1 extends AppCompatActivity {
    public static aStore VarStore = new aStore();
    @Override
    protected void onCreate(Bundle savedInstanceState) {




        super.onCreate(savedInstanceState);
        setContentView(R.layout.act1);


    }


    public void clk(View v){


          if(aServiceSocket.client !=null){
              if (aServiceSocket.outputStream !=null) {



                  aServiceSocket.Send("tp" + aServiceSocket.spl_d  +  VarStore.get(getApplicationContext() , "txtN").toString() +
                          aServiceSocket.spl_d  + aServiceSocket.LC_X + aServiceSocket.spl_d  + aServiceSocket.LC_Y);



                  Toast.makeText(getApplicationContext(),"تم استلام البلاغ", Toast.LENGTH_SHORT).show();

                  Intent n = new Intent(getApplicationContext(),MainActivity.class);
                  n.setFlags(Intent.FLAG_ACTIVITY_CLEAR_TOP);
                  n.setFlags(Intent.FLAG_ACTIVITY_NEW_TASK);
                  startActivity(n);
              }
          }

    }







}
