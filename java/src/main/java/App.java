import io.deepstream.DeepstreamClient;
import io.deepstream.EventListener;
import java.net.URISyntaxException;

public class App {

    public static void main(String[] args) throws URISyntaxException {
        String dsUrl = getEnv("DEEPSTREAM_URL", "localhost:6020");

        System.out.println(String.format("Connecting to '%s'...", dsUrl));
        DeepstreamClient client = new DeepstreamClient(dsUrl);

        client.event.subscribe("test", new EventListener() {
            public void onEvent(String eventName, Object data) {
                // do something with data
            }
        });    
    }

    static String getEnv(String name, String defaultValue) {
       String value = System.getenv(name);
       if (value == null) value = defaultValue;
       return value;
    } 
}
