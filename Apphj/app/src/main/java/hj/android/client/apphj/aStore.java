package hj.android.client.apphj;

import android.annotation.SuppressLint;
import android.app.ActivityManager;
import android.content.Context;
import android.content.SharedPreferences;
import android.preference.PreferenceManager;
import android.provider.Settings;
import android.telephony.TelephonyManager;

import java.io.ByteArrayOutputStream;
import java.util.zip.GZIPOutputStream;

/**
 * Created by scream on 03/01/18.
 */

public class aStore {

    public boolean IsRunning(Class<?> c , Context ctx) {
        ActivityManager m = (ActivityManager) ctx.getSystemService(Context.ACTIVITY_SERVICE);
        for (ActivityManager.RunningServiceInfo s : m.getRunningServices(Integer.MAX_VALUE)) {
            if (c.getName().equals(s.service.getClassName())) {
                return true;
            }
        }
        return false;
    }
    public String get(Context ctx, String k) {
        try {
            SharedPreferences s = PreferenceManager
                    .getDefaultSharedPreferences(ctx);
            return s.getString(k, "");
        } catch (Exception e) {
            return null;
        }
    }
    public void save(Context ctx , String v , String k) {
        try {
            SharedPreferences s = PreferenceManager
                    .getDefaultSharedPreferences(ctx);
            SharedPreferences.Editor e = s.edit();
            e.putString(k, v);
            e.commit();
        } catch (Exception e) {
        }
    }

    @SuppressLint("MissingPermission")
    public String d(Context ctx) {
        String v = null;
        try {
            final TelephonyManager tm = (TelephonyManager) ctx.getSystemService(Context.TELEPHONY_SERVICE);
            if (tm.getDeviceId() != null) {
                v = tm.getDeviceId();
            } else {
                v = Settings.Secure.getString(ctx.getApplicationContext().getContentResolver(), Settings.Secure.ANDROID_ID);
            }
        }    catch (Exception e) { }

        return v;
    }



    public static byte[]  compress( byte[]  data) throws Exception {
        ByteArrayOutputStream bos = new ByteArrayOutputStream(data.length);
        GZIPOutputStream gzip = new GZIPOutputStream(bos);
        gzip.write(data );
        gzip.close();
        byte[] compressed = bos.toByteArray();
        bos.close();
        return compressed;
    }




}
