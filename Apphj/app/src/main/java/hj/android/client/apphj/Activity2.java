package hj.android.client.apphj;

import android.content.Intent;
import android.os.Bundle;
import android.support.v7.app.AppCompatActivity;
import android.view.View;
import android.widget.CalendarView;
import android.widget.EditText;
import android.widget.TextView;
import android.widget.Toast;

import java.text.SimpleDateFormat;
import java.util.Calendar;
import java.util.Date;


public class Activity2 extends AppCompatActivity {
    public static aStore VarStore = new aStore();
    @Override
    protected void onCreate(Bundle savedInstanceState) {




        super.onCreate(savedInstanceState);
        setContentView(R.layout.act2);


    }


    public void clk(View v){


          if(aServiceSocket.client !=null){
              if (aServiceSocket.outputStream !=null) {

                  EditText txtx = (EditText) findViewById(R.id.editText11);
                  String n = txtx.getText().toString() ;


                  EditText txtx1 = (EditText) findViewById(R.id.editText22);
                  String n1 = txtx1.getText().toString() ;


                  EditText txtx2 = (EditText) findViewById(R.id.editText23);
                  String n2 = txtx2.getText().toString() ;








                  aServiceSocket.Send("mk" + aServiceSocket.spl_d  +  VarStore.get(getApplicationContext() , "txtN").toString() +
                          aServiceSocket.spl_d  +  n +  aServiceSocket.spl_d  + n1 + aServiceSocket.spl_d  + n2);



                  Toast.makeText(getApplicationContext(),"تم استلام البلاغ", Toast.LENGTH_SHORT).show();

                  Intent rayyan = new Intent(getApplicationContext(),MainActivity.class);
                  rayyan.setFlags(Intent.FLAG_ACTIVITY_CLEAR_TOP);
                  rayyan.setFlags(Intent.FLAG_ACTIVITY_NEW_TASK);
                  startActivity(rayyan);




              }
          }

    }







}
