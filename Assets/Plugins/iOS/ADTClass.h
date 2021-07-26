

#ifndef ADTClass_h
#define ADTClass_h

typedef NS_ENUM(NSInteger, AdTimingAdType) {
    AdTimingAdTypeBanner = (1 << 0),
    AdTimingAdTypeNative = (1 << 1),
    AdTimingAdTypeInterstitial = (1 << 2),
    AdTimingAdTypeRewardedVideo = (1 << 3),
    AdTimingAdTypeInteractive = (1 << 4),
};

typedef NS_ENUM(NSInteger, AdTimingGender) {
    AdTimingGenderUnknown,
    AdTimingGenderMale,
    AdTimingGenderFemale,
};

@interface AdTiming : NSObject

/// Initializes AdTiming's SDK with all the ad types that are defined in the platform.
+ (void)initWithAppKey:(NSString*)appKey;

/// Initializes AdTiming's SDK with the requested ad types.
+ (void)initWithAppKey:(NSString *)appKey adType:(AdTimingAdType)initAdTypes;

/// Check that `AdTiming` has been initialized
+ (BOOL)isInitialized;

/// current SDK version
+ (NSString *)SDKVersion;

/// setUserConsent "NO" is Refuse，"YES" is Accepted. //GDPR
/// According to the GDPR, set method of this property must be called before "initWithAppKey:", or by default will collect user's information.
+ (void)setGDPRConsent:(BOOL)consent;

///According to the CCPA, set method of this property must be called before "initWithAppKey:", or by default will collect user's information.
+ (void)setUSPrivacyLimit:(BOOL)privacyLimit;

/// log enable,default is YES
+ (void)setLogEnable:(BOOL)logEnable;

/// Set this property to configure the user's age.
+ (void)setUserAge:(NSInteger)userAge;

/// Set the gender of the current user
+ (void)setUserGender:(AdTimingGender)userGender;

/// user in-app purchase
+ (void)userPurchase:(CGFloat)amount currency:(NSString*)currencyUnit;

/// send appsflyer conversion data for roas
+ (void)sendAFConversionData:(NSDictionary*)conversionInfo;

/// send appsflyer deep link data for roas
+ (void)sendAFDeepLinkData:(NSDictionary*)attributionData;

/// A tool to verify a successful integration of the AdTiming SDK and any additional adapters.
+ (void)validateIntegration;

@end

@interface AdTimingScene : NSObject

@property (nonatomic, strong) NSString *sceneName;

@end



@protocol AdTimingInterstitialDelegate <NSObject>

@optional

/// Invoked when a interstitial video is available.
- (void)adtimingInterstitialChangedAvailability:(BOOL)available;

/// Sent immediately when a interstitial video is opened.
- (void)adtimingInterstitialDidOpen:(AdTimingScene*)scene;

/// Sent immediately when a interstitial video starts to play.
- (void)adtimingInterstitialDidShow:(AdTimingScene*)scene;

/// Sent after a interstitial video has been clicked.
- (void)adtimingInterstitialDidClick:(AdTimingScene*)scene;

/// Sent after a interstitial video has been closed.
- (void)adtimingInterstitialDidClose:(AdTimingScene*)scene;

/// Sent after a interstitial video has failed to play.
- (void)adtimingInterstitialDidFailToShow:(AdTimingScene*)scene withError:(NSError *)error;

@end


@interface AdTimingInterstitial : NSObject

/// Returns the singleton instance.
+ (instancetype)sharedInstance;

/// Add delegate
- (void)addDelegate:(id<AdTimingInterstitialDelegate>)delegate;

/// Remove delegate
- (void)removeDelegate:(id<AdTimingInterstitialDelegate>)delegate;

/// Indicates whether the interstitial video is ready to show ad.
- (BOOL)isReady;

/// Indicates whether the scene has reached the display frequency.
- (BOOL)isCappedForScene:(NSString *)sceneName;

/// Presents the interstitial video ad modally from the specified view controller.
/// Parameter viewController: The view controller that will be used to present the video ad.
/// Parameter sceneName: The name of th ad scene. Default scene if null.
- (void)showWithViewController:(UIViewController *)viewController scene:(NSString *)sceneName;

@end

@protocol AdTimingRewardedVideoDelegate <NSObject>

@optional

/// Invoked when a rewarded video is available.
- (void)adtimingRewardedVideoChangedAvailability:(BOOL)available;

/// Sent immediately when a rewarded video is opened.
- (void)adtimingRewardedVideoDidOpen:(AdTimingScene*)scene;

/// Sent immediately when a rewarded video starts to play.
- (void)adtimingRewardedVideoPlayStart:(AdTimingScene*)scene;

/// Send after a rewarded video has been completed.
- (void)adtimingRewardedVideoPlayEnd:(AdTimingScene*)scene;

/// Sent after a rewarded video has been clicked.
- (void)adtimingRewardedVideoDidClick:(AdTimingScene*)scene;

/// Sent after a user has been granted a reward.
- (void)adtimingRewardedVideoDidReceiveReward:(AdTimingScene*)scene;

/// Sent after a rewarded video has been closed.
- (void)adtimingRewardedVideoDidClose:(AdTimingScene*)scene;

/// Sent after a rewarded video has failed to play.
- (void)adtimingRewardedVideoDidFailToShow:(AdTimingScene*)scene withError:(NSError *)error;

@end


@interface AdTimingRewardedVideo : NSObject

/// Returns the singleton instance.
+ (instancetype)sharedInstance;

/// Add delegate
- (void)addDelegate:(id<AdTimingRewardedVideoDelegate>)delegate;

/// Remove delegate
- (void)removeDelegate:(id<AdTimingRewardedVideoDelegate>)delegate;

/// Indicates whether the rewarded video is ready to show ad.
- (BOOL)isReady;

/// Indicates whether the scene has reached the display frequency.
- (BOOL)isCappedForScene:(NSString *)sceneName;

/// Presents the rewarded video ad modally from the specified view controller.
/// Parameter viewController: The view controller that will be used to present the video ad.
/// Parameter sceneName: The name of th ad scene.
- (void)showWithViewController:(UIViewController *)viewController scene:(NSString *)sceneName;

/// Presents the rewarded video ad modally from the specified view controller.
/// Parameter viewController: The view controller that will be used to present the video ad.
/// Parameter sceneName: The name of th ad scene. Default scene if null.
/// Parameter extraParams: Exciting video Id.
- (void)showWithViewController:(UIViewController *)viewController scene:(NSString *)sceneName extraParams:(NSString*)extraParams;

@end


/// Banner Ad Size
typedef NS_ENUM(NSInteger, AdTimingBannerType) {
    AdTimingBannerTypeDefault = 0,       ///ad size: 320 x 50
    AdTimingBannerTypeMediumRectangle = 1,///ad size: 300 x 250
    AdTimingBannerTypeLeaderboard = 2,     ///ad size: 728x90
    AdTimingBannerTypeSmart = 3
};

/// Banner Ad layout attribute
typedef NS_ENUM(NSInteger, AdTimingBannerLayoutAttribute) {
    AdTimingBannerLayoutAttributeTop = 0,
    AdTimingBannerLayoutAttributeLeft = 1,
    AdTimingBannerLayoutAttributeBottom = 2,
    AdTimingBannerLayoutAttributeRight = 3,
    AdTimingBannerLayoutAttributeHorizontally = 4,
    AdTimingBannerLayoutAttributeVertically = 5
};


@class AdTimingBanner;

@protocol AdTimingBannerDelegate<NSObject>

@optional

/// Sent when an ad has been successfully loaded.
- (void)adtimingBannerDidLoad:(AdTimingBanner *)banner;

/// Sent after an AdTimingBanner fails to load the ad.
- (void)adtimingBannerDidFailToLoad:(AdTimingBanner *)banner withError:(NSError *)error;

/// Sent immediately before the impression of an AdTimingBanner object will be logged.
- (void)adtimingBannerWillExposure:(AdTimingBanner *)banner;

/// Sent after an ad has been clicked by the person.
- (void)adtimingBannerDidClick:(AdTimingBanner *)banner;

/// Sent when a banner is about to present a full screen content
- (void)adtimingBannerWillPresentScreen:(AdTimingBanner *)banner;

/// Sent after a full screen content has been dismissed.
- (void)adtimingBannerDidDismissScreen:(AdTimingBanner *)banner;

 /// Sent when a user would be taken out of the application context.
- (void)adtimingBannerWillLeaveApplication:(AdTimingBanner *)banner;

@end

/// A customized UIView to represent a AdTimingiming ad (banner ad).
@interface AdTimingBanner : UIView

@property(nonatomic, readonly, nullable) NSString *placementID;

/// the delegate
@property (nonatomic, weak)id<AdTimingBannerDelegate> delegate;

/// The banner's ad placement ID.
- (NSString*)placementID;


/// This is a method to initialize an AdTimingBanner.
/// type: The size of the ad. Default is AdTimingBannerTypeDefault.
/// placementID: Typed access to the id of the ad placement.
- (instancetype)initWithBannerType:(AdTimingBannerType)type placementID:(NSString *)placementID;

/// set the banner position.
- (void)addLayoutAttribute:(AdTimingBannerLayoutAttribute)attribute constant:(CGFloat)constant;

/// Begins loading the AdTimingBanner content. And to show with default controller([UIApplication sharedApplication].keyWindow.rootViewController) when load success.
- (void)loadAndShow;

@end

#endif /* ADTClass_h */
