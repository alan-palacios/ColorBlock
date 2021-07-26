//
//  ADTUnityBridge.m
//  Unity-iPhone
//
//  Created by ylm on 2019/7/31.
//

#import "ADTUnityBridge.h"


#define ADT_BANNER_POSITION_BOTTOM 0
#define ADT_BANNER_POSITION_TOP 1


#define ADTNSString2CString( str ) ( str != NULL && [str isKindOfClass:[NSString class]] ) ?  [str UTF8String] : ""


#define ADTCString2NSString( str ) ( str != NULL ) ? [NSString stringWithUTF8String:str] : [NSString stringWithUTF8String:""]

#ifdef __cplusplus
extern "C" {
#endif
  
extern void UnitySendMessage( const char *className, const char *methodName, const char *param );
    

#pragma mark SDK API
    
void adtLog(const char* log){
    NSLog(@"%s",log);
}

void adtSetLogEnable(bool logEnable)
{
    Class adtClass = NSClassFromString(@"AdTiming");
    if (adtClass && [adtClass respondsToSelector:@selector(setLogEnable)]) {
         [adtClass setLogEnable:logEnable];
    }
}


void adtInitWithAppKey(const char* appKey)
{
    NSString* key = ADTCString2NSString(appKey);
    [[ADTUnityBridge sharedInstance]initWithAppKey:key];
}

bool adtInitialized()
{
    bool initialized = false;
    Class adtClass = NSClassFromString(@"AdTiming");
    if (adtClass && [adtClass respondsToSelector:@selector(isInitialized)]) {
        initialized = [adtClass isInitialized];
    }
    return initialized;
}
    
void adtSetGDPRConsent(BOOL consent)
{
    Class adtClass = NSClassFromString(@"AdTiming");
    if (adtClass && [adtClass respondsToSelector:@selector(setGDPRConsent:)]) {
        [adtClass setGDPRConsent:consent];
    }
        
}
    
void adtSetIap(float count, const char* currency)
{
    NSString* curr = ADTCString2NSString(currency);
    Class adtClass = NSClassFromString(@"AdTiming");
    if (adtClass && [adtClass respondsToSelector:@selector(userPurchase:currency:)]) {
        [adtClass userPurchase:count currency:curr];
    }
}
    
void adtSetUserAge(int age)
{
    Class adtClass = NSClassFromString(@"AdTiming");
    if (adtClass && [adtClass respondsToSelector:@selector(setUserAge:)]) {
        [adtClass setUserAge:age];
    }
        
}
    
void adtSetUserGender(const char* gender)
{
    NSString* genderStr = ADTCString2NSString(gender);
        
    AdTimingGender userGender = AdTimingGenderUnknown;
    if ([genderStr isEqualToString:@"male"]) {
        userGender = AdTimingGenderMale;
    } else if ([genderStr isEqualToString:@"female"]) {
        userGender = AdTimingGenderFemale;
    }
        
    Class adtClass = NSClassFromString(@"AdTiming");
    if (adtClass && [adtClass respondsToSelector:@selector(setUserGender:)]) {
        [adtClass setUserGender:userGender];
    }
        
}
    
void adtSetUSPrivacyLimit(BOOL limit)
{
    Class adtClass = NSClassFromString(@"AdTiming");
    if (adtClass && [adtClass respondsToSelector:@selector(setUSPrivacyLimit:)]) {
        [adtClass setUSPrivacyLimit:limit];
    }
        
}
    
void adtSendAFConversionData(const char* conversionData)
{
if (conversionData) {
    NSString *cd = ADTCString2NSString(conversionData);
    if ([cd length] > 0) {
        NSData *data = [cd dataUsingEncoding:NSUTF8StringEncoding];
        NSError *jsonErr = nil;
        NSDictionary *dict = [NSJSONSerialization JSONObjectWithData:data options:NSJSONReadingAllowFragments error:&jsonErr];
        if (!jsonErr && [dict isKindOfClass:[NSDictionary class]]) {
                Class adtClass = NSClassFromString(@"AdTiming");
            if (adtClass && [adtClass respondsToSelector:@selector(sendAFConversionData:)]) {
                [adtClass sendAFConversionData:dict];
            }

        }
    }
 }

}

void adtSendAFDeepLinkData(const char* attributionData){
    if (attributionData) {
        NSString *cd = ADTCString2NSString(attributionData);
        if ([cd length] > 0) {
            NSData *data = [cd dataUsingEncoding:NSUTF8StringEncoding];
            NSError *jsonErr = nil;
            NSDictionary *dict = [NSJSONSerialization JSONObjectWithData:data options:NSJSONReadingAllowFragments error:&jsonErr];
            if (!jsonErr && [dict isKindOfClass:[NSDictionary class]]) {
                Class adtClass = NSClassFromString(@"AdTiming");
                if (adtClass && [adtClass respondsToSelector:@selector(sendAFDeepLinkData:)]) {
                    [adtClass sendAFDeepLinkData:dict];
                }
            }
        }
    }
}

#pragma mark Interstitial API
    
void adtShowInterstitial(){
    if ([[ADTUnityBridge sharedInstance] interstitialIsReady]) {
        [[ADTUnityBridge sharedInstance] showInterstitial];
    }
}

void adtShowInterstitialWithScene(const char* scene){
    NSString* adtScene = [NSString stringWithUTF8String:scene];
    if ([[ADTUnityBridge sharedInstance] interstitialIsReady]) {
        [[ADTUnityBridge sharedInstance] showInterstitialWithScene:adtScene];
    }
}

bool adtInterstitialIsReady(){
    return [[ADTUnityBridge sharedInstance] interstitialIsReady];
}

#pragma mark RewardedVideo API

void adtShowRewardedVideo(){
    if ([[ADTUnityBridge sharedInstance] videoIsReady]) {
        [[ADTUnityBridge sharedInstance] showVideo];
    }
}

void adtShowRewardedVideoWithScene(const char* scene){
    NSString* adtScene = [NSString stringWithUTF8String:scene];
    if ([[ADTUnityBridge sharedInstance] videoIsReady]) {
        [[ADTUnityBridge sharedInstance] showVideoWithScene:adtScene];
    }
}

void adtShowRewardedVideoWithExtraParams(const char* scene, const char* extraParams){
    NSString* adtScene = [NSString stringWithUTF8String:scene];
    NSString* adtParams = [NSString stringWithUTF8String:extraParams];
    if ([[ADTUnityBridge sharedInstance] videoIsReady]) {
        [[ADTUnityBridge sharedInstance] showVideoWithExtraParams:adtScene extraParams:adtParams];
    }
}

bool adtRewardedVideoIsReady(){
    return [[ADTUnityBridge sharedInstance] videoIsReady];
}
    
#pragma mark Banner API
    
void adtLoadBanner(int bannerType, int position, char* placementId)
{
    [[ADTUnityBridge sharedInstance] loadBanner:bannerType position:position placement:ADTCString2NSString(placementId)];
}

void adtDestroyBanner (char* placementId)
{
    [[ADTUnityBridge sharedInstance] destroyBanner:ADTCString2NSString(placementId)];
}

void adtDisplayBanner (char* placementId)
{
    [[ADTUnityBridge sharedInstance] displayBanner:ADTCString2NSString(placementId)];
}

void adtHideBanner (char* placementId)
{
    [[ADTUnityBridge sharedInstance] hideBanner:ADTCString2NSString(placementId)];
}


#ifdef __cplusplus
}
#endif

static ADTUnityBridge * _instance = nil;

@implementation ADTUnityBridge

char *const kAdTimingEvents = "AdTimingEvents";

+ (instancetype)sharedInstance {
    static dispatch_once_t onceToken;
    dispatch_once(&onceToken, ^{
        _instance = [[self alloc] init];
    });
    return _instance;
}

- (instancetype)init{
    if (self = [super init]) {
        _bannerAdMap = [NSMutableDictionary dictionary];
        _bannerPostionMap = [NSMutableDictionary dictionary];
        
        [[NSNotificationCenter defaultCenter] addObserver:self selector:@selector(initSuccess) name:@"AdTiming_INIT_SUCCESS" object:nil];
        
        Class interstitialClass = NSClassFromString(@"AdTimingInterstitial");
        if(interstitialClass && [interstitialClass respondsToSelector:@selector(sharedInstance)] && [interstitialClass instancesRespondToSelector:@selector(addDelegate:)]) {
            [[interstitialClass sharedInstance] addDelegate:self];
        }
        
        Class rewardedVideoClass = NSClassFromString(@"AdTimingRewardedVideo");
        if(rewardedVideoClass && [rewardedVideoClass respondsToSelector:@selector(sharedInstance)] && [rewardedVideoClass instancesRespondToSelector:@selector(addDelegate:)]) {
            [[rewardedVideoClass sharedInstance] addDelegate:self];
        }
        
        [[NSNotificationCenter defaultCenter] addObserver:self selector:@selector(orientationChanged:)
                                                     name:UIDeviceOrientationDidChangeNotification object:nil];
    }
    return self;
}

#pragma mark -- init

- (void)initWithAppKey:(NSString*)appKey {
    Class adtClass = NSClassFromString(@"AdTiming");
    if (adtClass && [adtClass respondsToSelector:@selector(initWithAppKey:)]) {
        [adtClass initWithAppKey:appKey];
    }
}

- (void)initSuccess {
    UnitySendMessage(kAdTimingEvents,"onSdkInitSuccess","");
}

#pragma mark -- interstitial

- (BOOL)interstitialIsReady {
    BOOL isReady = NO;
    Class interstitialClass = NSClassFromString(@"AdTimingInterstitial");
    if(interstitialClass && [interstitialClass respondsToSelector:@selector(sharedInstance)] && [interstitialClass instancesRespondToSelector:@selector(isReady)]) {
        isReady = [[interstitialClass sharedInstance] isReady];
    }
    return isReady;
}

- (void)showInterstitial {
    Class interstitialClass = NSClassFromString(@"AdTimingInterstitial");
    if(interstitialClass && [interstitialClass respondsToSelector:@selector(sharedInstance)] && [interstitialClass instancesRespondToSelector:@selector(showWithViewController:scene:)]) {
        [[interstitialClass sharedInstance] showWithViewController:[UIApplication sharedApplication].keyWindow.rootViewController scene:@""];
    }
}

- (void)showInterstitialWithScene:(NSString *)scene {
    Class interstitialClass = NSClassFromString(@"AdTimingInterstitial");
    if(interstitialClass && [interstitialClass respondsToSelector:@selector(sharedInstance)] && [interstitialClass instancesRespondToSelector:@selector(showWithViewController:scene:)]) {
        [[interstitialClass sharedInstance] showWithViewController:[UIApplication sharedApplication].keyWindow.rootViewController scene:scene];
    }
}

/// Invoked when a interstitial video is available.
- (void)adtimingInterstitialChangedAvailability:(BOOL)available {
    
    UnitySendMessage(kAdTimingEvents,"onInterstitialAvailabilityChanged",(available) ? "true" : "false");
}

/// Sent immediately when a interstitial video is opened.
- (void)adtimingInterstitialDidOpen:(AdTimingScene*)scene {

}

/////////////
/// Sent immediately when a interstitial video starts to play.
- (void)adtimingInterstitialDidShow:(AdTimingScene*)scene {
    
    UnitySendMessage(kAdTimingEvents,"onInterstitialShowed",ADTNSString2CString(scene.sceneName));
}

/// Sent after a interstitial video has been clicked.
- (void)adtimingInterstitialDidClick:(AdTimingScene*)scene {
    
    UnitySendMessage(kAdTimingEvents,"onInterstitialClicked",ADTNSString2CString(scene.sceneName));
}

/// Sent after a interstitial video has been closed.
- (void)adtimingInterstitialDidClose:(AdTimingScene*)scene {

    UnitySendMessage(kAdTimingEvents,"onInterstitialClosed",ADTNSString2CString(scene.sceneName));
    
}

/// Sent after a interstitial video has failed to play.
- (void)adtimingInterstitialDidFailToShow:(AdTimingScene*)scene withError:(NSError *)error {

    if (error) {
        NSString *errorStr = [NSString stringWithFormat:@"error code:%ld msg:%@", (long)[error code],[error localizedDescription]];
            UnitySendMessage(kAdTimingEvents,"onInterstitialShowFailed",ADTNSString2CString(errorStr));
    } else {
         UnitySendMessage(kAdTimingEvents,"onInterstitialShowFailed","");
    }
    
}


#pragma mark -- video
- (BOOL)videoIsReady {
    BOOL isReady = NO;
    Class rewardedVideoClass = NSClassFromString(@"AdTimingRewardedVideo");
    if(rewardedVideoClass && [rewardedVideoClass respondsToSelector:@selector(sharedInstance)] && [rewardedVideoClass instancesRespondToSelector:@selector(isReady)]) {
        isReady = [[rewardedVideoClass sharedInstance] isReady];
    }
    return isReady;
}

- (void)showVideo {
    Class rewardedVideoClass = NSClassFromString(@"AdTimingRewardedVideo");
    if(rewardedVideoClass && [rewardedVideoClass respondsToSelector:@selector(sharedInstance)] && [rewardedVideoClass instancesRespondToSelector:@selector(showWithViewController:scene:)]) {
        [[rewardedVideoClass sharedInstance] showWithViewController:[UIApplication sharedApplication].keyWindow.rootViewController scene:@""];
    }
}

- (void)showVideoWithScene:(NSString *)scene {
    Class rewardedVideoClass = NSClassFromString(@"AdTimingRewardedVideo");
    if(rewardedVideoClass && [rewardedVideoClass respondsToSelector:@selector(sharedInstance)] && [rewardedVideoClass instancesRespondToSelector:@selector(showWithViewController:scene:)]) {
        [[rewardedVideoClass sharedInstance] showWithViewController:[UIApplication sharedApplication].keyWindow.rootViewController scene:scene];
    }
}

- (void)showVideoWithExtraParams:(NSString *)scene extraParams:(NSString *)extraParams {
    Class rewardedVideoClass = NSClassFromString(@"AdTimingRewardedVideo");
    if(rewardedVideoClass && [rewardedVideoClass respondsToSelector:@selector(sharedInstance)] && [rewardedVideoClass instancesRespondToSelector:@selector(showWithViewController:scene:extraParams:)]) {
    [[rewardedVideoClass sharedInstance] showWithViewController:[UIApplication sharedApplication].keyWindow.rootViewController scene:scene extraParams:extraParams];
    }
}

#pragma mark -- AdTimingRewardedVideoDelegate

- (void)adtimingRewardedVideoChangedAvailability:(BOOL)available {
    
    UnitySendMessage(kAdTimingEvents,"onRewardedVideoAvailabilityChanged",(available) ? "true" : "false");
}

- (void)adtimingRewardedVideoDidOpen:(AdTimingScene*)scene {

    UnitySendMessage(kAdTimingEvents,"onRewardedVideoShowed",ADTNSString2CString(scene.sceneName));
}

- (void)adtimingRewardedVideoPlayStart:(AdTimingScene *)scene {

    UnitySendMessage(kAdTimingEvents,"onRewardedVideoStarted",ADTNSString2CString(scene.sceneName));
}

- (void)adtimingRewardedVideoDidClick:(AdTimingScene *)scene {

    UnitySendMessage(kAdTimingEvents,"onRewardedVideoClicked",ADTNSString2CString(scene.sceneName));
}

- (void)adtimingRewardedVideoDidClose:(AdTimingScene *)scene {

    UnitySendMessage(kAdTimingEvents,"onRewardedVideoClosed",ADTNSString2CString(scene.sceneName));
}

- (void)adtimingRewardedVideoPlayEnd:(AdTimingScene*)scene {

    UnitySendMessage(kAdTimingEvents,"onRewardedVideoEnded",ADTNSString2CString(scene.sceneName));
}

- (void)adtimingRewardedVideoDidReceiveReward:(AdTimingScene*)scene {

    UnitySendMessage(kAdTimingEvents,"onRewardedVideoRewarded",ADTNSString2CString(scene.sceneName));
    
}

- (void)adtimingRewardedVideoDidFailToShow:(AdTimingScene *)scene withError:(NSError *)error {

    if (error) {
        NSString *errorStr = [NSString stringWithFormat:@"error code:%ld msg:%@", (long)[error code],[error localizedDescription]];
            UnitySendMessage(kAdTimingEvents,"onRewardedVideoShowFailed",ADTNSString2CString(errorStr));
    } else {
         UnitySendMessage(kAdTimingEvents,"onRewardedVideoShowFailed","");
    }
}


#pragma mark Banner API

- (void)loadBanner:(NSInteger)bannerType position:(NSInteger)position placement:(NSString *)placementId {
    @synchronized(self) {
            AdTimingBanner *banner = [_bannerAdMap objectForKey:placementId];
            Class bannerClass =  NSClassFromString(@"AdTimingBanner");
            if (!banner && bannerClass && [bannerClass instancesRespondToSelector:@selector(initWithBannerType:placementID:)]) {
                banner = [[bannerClass alloc]initWithBannerType:bannerType placementID:placementId];
                banner.delegate = self;
            }
            if (banner) {
                [_bannerAdMap setObject:banner forKey:placementId];
                [_bannerPostionMap setObject:[NSNumber numberWithInteger:position] forKey:placementId];
                banner.center = [self getBannerCenter:banner position:position];
                [[UIApplication sharedApplication].keyWindow.rootViewController.view addSubview:banner];
                [banner loadAndShow];
            }

            
    }
}

- (void)destroyBanner:(NSString*)placementId {
    dispatch_async(dispatch_get_main_queue(), ^{
        @synchronized(self) {
            AdTimingBanner *banner = [_bannerAdMap objectForKey:placementId];
            if (banner != nil) {
                [banner removeFromSuperview];
            }
            [_bannerAdMap removeObjectForKey:placementId];
            [_bannerPostionMap removeObjectForKey:placementId];
        }
    });
}

- (void)displayBanner:(NSString*)placementId {
    dispatch_async(dispatch_get_main_queue(), ^{
        @synchronized(self) {
            AdTimingBanner *banner = [_bannerAdMap objectForKey:placementId];
            if (banner != nil) {
                [banner setHidden:NO];
            }
        }
    });
}

- (void)hideBanner:(NSString*)placementId {
    dispatch_async(dispatch_get_main_queue(), ^{
        @synchronized(self) {
            AdTimingBanner *banner = [_bannerAdMap objectForKey:placementId];
            if (banner != nil) {
                [banner setHidden:YES];
            }
        }
    });
}

- (CGPoint)getBannerCenter:(AdTimingBanner*)bannerView position:(NSInteger)position {
    CGFloat y;
    UIView *topView = [UIApplication sharedApplication].keyWindow.rootViewController.view;
    if (position == ADT_BANNER_POSITION_TOP) {
        y = (bannerView.frame.size.height / 2);
        if (@available(ios 11.0, *)) {
            y += topView.safeAreaInsets.top;
        }
    } else {
        y = topView.frame.size.height - (bannerView.frame.size.height / 2);
        if (@available(ios 11.0, *)) {
            y -= topView.safeAreaInsets.bottom;
        }
    }
    
    return CGPointMake(topView.frame.size.width / 2, y);
}

- (void)centerBanner {
    dispatch_async(dispatch_get_main_queue(), ^{
        @synchronized(self) {
            NSArray *placements = [_bannerAdMap allKeys];
            for (NSString *placementId in placements) {
                AdTimingBanner *banner = [_bannerAdMap objectForKey:placementId];
                NSInteger postion = [[_bannerPostionMap objectForKey:placementId]integerValue];
                banner.center = [self getBannerCenter:banner position:postion];
            }
        }
    });
}

- (void)orientationChanged:(NSNotification *)notification {
    [self centerBanner];
}


#pragma mark Banner Delegate

- (void)adtimingBannerDidLoad:(AdTimingBanner *)banner {
    UnitySendMessage(kAdTimingEvents, "onBannerLoadSuccess", "");
}


- (void)adtimingBannerDidFailToLoad:(AdTimingBanner *)banner withError:(NSError *)error {
    if (error) {
        NSString *errorStr = [NSString stringWithFormat:@"error code:%ld msg:%@", (long)[error code],[error localizedDescription]];
        UnitySendMessage(kAdTimingEvents, "onBannerLoadFailed",[errorStr UTF8String] );
    } else {
        UnitySendMessage(kAdTimingEvents, "onBannerLoadFailed", "");
    }
}


- (void)adtimingBannerWillExposure:(AdTimingBanner *)banner {
    //UnitySendMessage(kAdTimingEvents, "onBannerShow", "");
}


- (void)adtimingBannerDidClick:(AdTimingBanner *)banner {
    UnitySendMessage(kAdTimingEvents, "onBannerClicked", "");
}


- (void)adtimingBannerWillPresentScreen:(AdTimingBanner *)banner {
    //UnitySendMessage(kAdTimingEvents, "onBannerScreenPresented", "");
}


- (void)adtimingBannerDidDismissScreen:(AdTimingBanner *)banner {
    //UnitySendMessage(kAdTimingEvents, "onBannerScreenDismissed", "");
}


- (void)adtimingBannerWillLeaveApplication:(AdTimingBanner *)banner {
    //UnitySendMessage(kAdTimingEvents, "onBannerLeftApplication", "");
}


@end
