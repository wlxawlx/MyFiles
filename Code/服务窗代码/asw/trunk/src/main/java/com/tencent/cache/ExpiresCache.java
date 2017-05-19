package com.tencent.cache;


import org.apache.ibatis.cache.Cache;
import org.apache.ibatis.cache.impl.PerpetualCache;

import java.util.HashMap;
import java.util.Map;
import java.util.concurrent.locks.ReadWriteLock;

/**
 * Created by zhufengxiang on 2016/09/06.
 * 腾讯文档中建议采用全局的缓存来保存 access token 和 js api ticket。
 *
 * 实现缓存的有效时间。
 */
public class ExpiresCache implements Cache {

    private Cache delegate;
    private long expires = 7200;  // 缓存失效时间  单位: s.
    private Map<Object, Long> startTime = new HashMap<>();

    public ExpiresCache(Cache delegate) {
        this.delegate = delegate;
    }

    public ExpiresCache(Cache delegate, long expires) {
        this.delegate = delegate;
        this.expires = expires;
    }

    public ExpiresCache(long expires) {
        this.delegate = new PerpetualCache("default4expiresCache");
        this.expires = expires;
    }

    @Override
    public String getId() {
        return delegate.getId();
    }

    @Override
    public void putObject(Object key, Object value) {
        long current = currentTimeInSecond();
        startTime.put(key, current);
        delegate.putObject(key, value);
    }

    @Override
    public Object getObject(Object key) {
        if(!expired(key)) {
            return delegate.getObject(key);
        }
        return null;
    }

    @Override
    public Object removeObject(Object key) {
        startTime.remove(key);
        return delegate.removeObject(key);
    }

    @Override
    public void clear() {
        startTime.clear();
        delegate.clear();
    }

    @Override
    public int getSize() {
        return delegate.getSize();
    }

    // 可选实现
    @Override
    public ReadWriteLock getReadWriteLock() {
        return null;
    }

    private long currentTimeInSecond() {
        return System.currentTimeMillis() / 1000;
    }

    public boolean expired(Object key) {
        if (startTime.get(key) == null) {
            return true;
        }
        // 提前50秒失效
        return (currentTimeInSecond() - startTime.get(key)) >= (expires - 50);
    }

    public static void main(String[] args) throws InterruptedException {
        PerpetualCache perpetualCache = new PerpetualCache("fortx");
        ExpiresCache expiresCache = new ExpiresCache(perpetualCache, 60);
        System.out.println(expiresCache.getObject("access token"));

        expiresCache.putObject("access token", "hello world");
        System.out.println("存入后立即获取值 - "+expiresCache.getObject("access token"));

        Thread.sleep(9000);

        System.out.println("睡9秒后的结果 - "+expiresCache.getObject("access token"));

        Thread.sleep(2000);

        System.out.println("再睡2秒后的结果 - "+expiresCache.getObject("access token"));
    }

}
