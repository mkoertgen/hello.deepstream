import com.google.gson.JsonObject;
import io.deepstream.DeepstreamClient;
import io.deepstream.EventListener;
import io.deepstream.RpcResult;
import java.net.URISyntaxException;

public class App {

    public static void main(String[] args) 
    throws URISyntaxException, InterruptedException {
        String dsUrl = getEnv("DEEPSTREAM_URL", "localhost:6020");

        System.out.println(String.format("Connecting to '%s'...", dsUrl));
        DeepstreamClient client = new DeepstreamClient(dsUrl);

        //String user = getEnv("DEEPSTREAM_USER", "userA"); 
        //String pwd = getEnv("DEEPSTREAM_PWD", "pwd"); 
        client.login();
        System.out.println("Logged in.");
        
        String eventName = getEnv("DEEPSTREAM_EVENT", "test");

        client.event.subscribe(eventName, new EventListener() {
            public void onEvent(String eventName, Object data) {
                System.out.println(data);
            }
        });    
        System.out.println(String.format("Subscribed to '%s'.", eventName));
        Thread.sleep(1000);

        final String data = "Hello from Java";
        System.out.println(String.format("Publishing '%s': '%s'...", eventName, data));
        client.event.emit(eventName, data);

        JsonObject jsonObject = new JsonObject();
        jsonObject.addProperty("a", 7);
        jsonObject.addProperty("b", 8);
        RpcResult rpcResult = client.rpc.make( "/nodejs/multiply-numbers", jsonObject);
        float result = (Float) rpcResult.getData(); // 56.0
        System.out.println(String.format("Result: %s", result));

        while (true) Thread.sleep(1000);
    }

    static String getEnv(String name, String defaultValue) {
       String value = System.getenv(name);
       if (value == null) value = defaultValue;
       return value;
    } 
}
