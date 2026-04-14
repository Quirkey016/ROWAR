/////////////////////////////////////////////////////////////////////////////////////////////////////
//
// Audiokinetic Wwise generated include file. Do not edit.
//
/////////////////////////////////////////////////////////////////////////////////////////////////////

#ifndef __WWISE_IDS_H__
#define __WWISE_IDS_H__

#include <AK/SoundEngine/Common/AkTypes.h>

namespace AK
{
    namespace EVENTS
    {
        static const AkUniqueID FIREBOLT_IMPACT = 3407841817U;
        static const AkUniqueID FS_PLAYER = 157611648U;
        static const AkUniqueID STAFF_FIREBOLT_CAST = 2128022953U;
        static const AkUniqueID START_FIREBOLT_MIDAIR = 1975303464U;
        static const AkUniqueID STOP_FIREBOLT_MIDAIR = 692113986U;
    } // namespace EVENTS

    namespace STATES
    {
        namespace SPELLCASTING_STATES
        {
            static const AkUniqueID GROUP = 73172129U;

            namespace STATE
            {
                static const AkUniqueID NONE = 748895195U;
                static const AkUniqueID SPELLCASTING_GRIMOIRE = 3956951341U;
                static const AkUniqueID SPELLCASTING_STAFF = 2470714719U;
            } // namespace STATE
        } // namespace SPELLCASTING_STATES

        namespace WALKING_SURFACE
        {
            static const AkUniqueID GROUP = 2891784818U;

            namespace STATE
            {
                static const AkUniqueID DIRT = 2195636714U;
                static const AkUniqueID GRASS = 4248645337U;
                static const AkUniqueID MARBLE = 1127618254U;
                static const AkUniqueID NONE = 748895195U;
                static const AkUniqueID STONE = 1216965916U;
                static const AkUniqueID WOOD = 2058049674U;
            } // namespace STATE
        } // namespace WALKING_SURFACE

    } // namespace STATES

    namespace SWITCHES
    {
        namespace SURFACETYPE
        {
            static const AkUniqueID GROUP = 63790334U;

            namespace SWITCH
            {
                static const AkUniqueID GRASS = 4248645337U;
                static const AkUniqueID HAY = 982177701U;
            } // namespace SWITCH
        } // namespace SURFACETYPE

    } // namespace SWITCHES

    namespace GAME_PARAMETERS
    {
        static const AkUniqueID BUS_VOLUME_FOOTSTEPS = 4073739390U;
    } // namespace GAME_PARAMETERS

    namespace BANKS
    {
        static const AkUniqueID INIT = 1355168291U;
        static const AkUniqueID IMPLEMENTED_SOUNDS = 543157906U;
    } // namespace BANKS

    namespace BUSSES
    {
        static const AkUniqueID ENEMIES = 2242381963U;
        static const AkUniqueID FOOTSTEPS = 2385628198U;
        static const AkUniqueID MASTER_AUDIO_BUS = 3803692087U;
        static const AkUniqueID PLAYER = 1069431850U;
        static const AkUniqueID SPELLS = 4160437992U;
    } // namespace BUSSES

    namespace AUDIO_DEVICES
    {
        static const AkUniqueID NO_OUTPUT = 2317455096U;
        static const AkUniqueID SYSTEM = 3859886410U;
    } // namespace AUDIO_DEVICES

}// namespace AK

#endif // __WWISE_IDS_H__
