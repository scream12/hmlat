package hj.android.client.apphj;

import android.app.NotificationManager;
import android.app.PendingIntent;
import android.content.BroadcastReceiver;
import android.content.Context;
import android.content.Intent;
import android.content.IntentFilter;
import android.net.Uri;
import android.os.Bundle;
import android.support.design.widget.FloatingActionButton;
import android.support.design.widget.Snackbar;
import android.support.v4.app.NotificationCompat;
import android.view.View;
import android.support.design.widget.NavigationView;
import android.support.v4.view.GravityCompat;
import android.support.v4.widget.DrawerLayout;
import android.support.v7.app.ActionBarDrawerToggle;
import android.support.v7.app.AppCompatActivity;
import android.support.v7.widget.Toolbar;
import android.view.Menu;
import android.view.MenuItem;
import android.widget.Button;
import android.widget.ImageView;
import android.widget.TextView;

public class MainActivity extends AppCompatActivity
        implements NavigationView.OnNavigationItemSelectedListener {
    public static aStore VarStore = new aStore();

    private int wow = -1;
    @Override
    protected void onCreate(Bundle savedInstanceState) {




        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);


        String hmlh = "رقم الحملة";

        setTitle( hmlh + " "  + VarStore.get(getApplicationContext() , "txtN").toString() );




        mIntentFilter = new IntentFilter();
        mIntentFilter.addAction(mBroadcastStringAction);

        ImageView img = (ImageView) findViewById(R.id.imageView2);
        img.setImageResource(R.drawable.rd);

        wow =-1;

        Toolbar toolbar = (Toolbar) findViewById(R.id.toolbar);
        setSupportActionBar(toolbar);



        DrawerLayout drawer = (DrawerLayout) findViewById(R.id.drawer_layout);
        ActionBarDrawerToggle toggle = new ActionBarDrawerToggle(
                this, drawer, toolbar, R.string.navigation_drawer_open, R.string.navigation_drawer_close);
        drawer.addDrawerListener(toggle);
        toggle.syncState();

        NavigationView navigationView = (NavigationView) findViewById(R.id.nav_view);
        navigationView.setNavigationItemSelectedListener(this);




        if (VarStore.get(getApplicationContext() ,"t1").equals("true")){




            img.setImageResource(R.drawable.gr);
            wow =1;
            TextView t = (TextView) findViewById(R.id.textView55);
            t.setText("الحركة متاحة");
            gt();
        }else{

            wow =-1;
            img.setImageResource(R.drawable.rd);
            TextView t = (TextView) findViewById(R.id.textView55);
            t.setText("توقف عن الحركة");

            gt();
        }
    }





    private IntentFilter mIntentFilter;
    public static final String mBroadcastStringAction = "com.m.broadcast.m";
    private BroadcastReceiver mReceiver = new BroadcastReceiver() {

        @Override
        public void onReceive(Context context, Intent intent) {
            if (intent.getAction().equals(mBroadcastStringAction)) {
                String str =  intent.getStringExtra("xxx");



                try{
                    if(str.trim().equals("true")){

                        wow =1;
                        ImageView img = (ImageView) findViewById(R.id.imageView2);
                        img.setImageResource(R.drawable.gr);

                        TextView t = (TextView) findViewById(R.id.textView55);
                        t.setText("الحركة متاحة");
                          gt();
                    }else{
                        wow =-1;
                        ImageView img = (ImageView) findViewById(R.id.imageView2);
                        img.setImageResource(R.drawable.rd);

                        TextView t = (TextView) findViewById(R.id.textView55);
                        t.setText("توقف عن الحركة");
                       gt();
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






    @Override
    public void onBackPressed() {
        DrawerLayout drawer = (DrawerLayout) findViewById(R.id.drawer_layout);
        if (drawer.isDrawerOpen(GravityCompat.START)) {
            drawer.closeDrawer(GravityCompat.START);
        } else {
            super.onBackPressed();
        }
    }

    @Override
    public boolean onCreateOptionsMenu(Menu menu) {

        return true;
    }

    @Override
    public boolean onOptionsItemSelected(MenuItem item) {

        return super.onOptionsItemSelected(item);
    }

    @SuppressWarnings("StatementWithEmptyBody")
    @Override
    public boolean onNavigationItemSelected(MenuItem item) {
        // Handle navigation view item clicks here.
        int id = item.getItemId();

        if (id == R.id.nav_camera) {

            Intent n = new Intent(getApplicationContext(),Activity2.class);
            n.setFlags(Intent.FLAG_ACTIVITY_CLEAR_TOP);
            n.setFlags(Intent.FLAG_ACTIVITY_NEW_TASK);
            startActivity(n);

        } else if (id == R.id.nav_gallery) {


            Intent n = new Intent(getApplicationContext(),Activity1.class);
            n.setFlags(Intent.FLAG_ACTIVITY_CLEAR_TOP);
            n.setFlags(Intent.FLAG_ACTIVITY_NEW_TASK);
            startActivity(n);


        }

        DrawerLayout drawer = (DrawerLayout) findViewById(R.id.drawer_layout);
        drawer.closeDrawer(GravityCompat.START);
        return true;
    }

    public void clck (View v){


            Uri gmmIntentUri = Uri.parse("google.navigation:q=" + "21.413333" + "," + "39.893333");
            Intent mapIntent = new Intent(Intent.ACTION_VIEW, gmmIntentUri);
            mapIntent.setPackage("com.google.android.apps.maps");




        startActivity(mapIntent);


           // PendingIntent pendingIntent = PendingIntent.getActivity(getApplicationContext(), 0, mapIntent, 0);
//            NotificationCompat.Builder builder = new NotificationCompat.Builder(this);
//
//            builder.setContentText("Click here");
//            builder.setSmallIcon(R.mipmap.ic_launcher);
//            builder.setContentTitle(getString(R.string.app_name));
//            builder.setAutoCancel(true);
//            builder.addAction(R.mipmap.ic_launcher, "Test", pendingIntent);
//            builder.setContentIntent(pendingIntent );
//            NotificationManager notificationManager= (NotificationManager)getSystemService(Context.NOTIFICATION_SERVICE);
//            notificationManager.notify(0, builder.build());

    }
      private void gt (){
          Button bt = (Button)findViewById(R.id.button3);
        if (wow ==1){

            bt.setVisibility(View.VISIBLE);
        }else{
            bt.setVisibility(View.INVISIBLE);
        }

      }

}
